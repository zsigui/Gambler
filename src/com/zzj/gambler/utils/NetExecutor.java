package com.zzj.gambler.utils;

import java.lang.ref.WeakReference;
import java.util.HashMap;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.Semaphore;
import java.util.concurrent.ThreadFactory;
import java.util.concurrent.atomic.AtomicInteger;

import com.sun.xml.internal.ws.api.pipe.NextAction;

public class NetExecutor {

	private static final int DEFAULT_THREAD_SIZE = 5;
	// 默认超时时间：10s
	private static final int DEFAULT_OVERTIME = 10_000;

	private volatile boolean isShutdown = false;
	private Semaphore mSemaphore;
	private ConcurrentLinkedQueue<Runnable> mWaitQueue;
	private ConcurrentHashMap<Integer, Thread> mWorkingMap;

	// 依赖参数
	private int mFixedThreadSize = DEFAULT_THREAD_SIZE;
	private int mOverTime = DEFAULT_OVERTIME;

	public NetExecutor() {
		this(DEFAULT_THREAD_SIZE, DEFAULT_OVERTIME);
	}
	
	public NetExecutor(int threadSize, int overtime) {
		mFixedThreadSize = threadSize;
		mOverTime = overtime;
		mSemaphore = new Semaphore(mFixedThreadSize);
		mWaitQueue = new ConcurrentLinkedQueue<>();
		mWorkingMap = new ConcurrentHashMap<>(mFixedThreadSize);
	}
	
	public void execute(Runnable r) {
		if (r == null)
			throw new IllegalArgumentException("传入参数不能为 null");
		if (isShutdown)
			throw new IllegalStateException("该线程池已经关闭，请重新初始化实例");
		mWaitQueue.add(r);
		realExecute();
	}

	public void stop(Runnable r) {
		if (r == null) return;
		
		if (mWorkingMap.containsKey(r.hashCode())) {
			// 通知任务终止
			Thread t = mWorkingMap.get(r.hashCode());
			if (t.isAlive() && !t.isInterrupted()) {
				t.interrupt();
			}
		} else if (mWaitQueue.contains(r)) {
			// 任务还没有运行，直接从等待列表中移除即可
			mWaitQueue.remove(r);
		}
		
	}
	
	public void shutdown() {
		isShutdown = true;
		for (Thread thread : mWorkingMap.values()) {
			if (thread.isAlive() && !thread.isInterrupted())
				thread.interrupt();
		}
		mWorkingMap.clear();
		mWorkingMap = null;
		mWaitQueue.clear();
		mWaitQueue = null;
	}
	
	private void realExecute() {
		if (mSemaphore.tryAcquire()) {
			Runnable r = mWaitQueue.poll();
			if (r == null)
				return;
			
			ObserverThread thread = new ObserverThread(mOverTime, new ObservedThread(r));
			mWorkingMap.put(r.hashCode(), thread);
			thread.start();
		}
	}

	/**
	 * 监视线程，当内部线程超时时执行强制中断
	 */
	class ObserverThread extends Thread {

		private int _overtime;
		private ObservedThread _observed;
		private boolean _isObservedFinished = false;

		public ObserverThread(int outtime, ObservedThread observed) {
			super();
			this._overtime = outtime;
			this._observed = observed;
			this._observed.setObserver(this);
		}

		public void setObservedFinished(boolean isObservedFinished) {
			_isObservedFinished = isObservedFinished;
		}

		@Override
		public synchronized void start() {
			_observed.run();
			super.start();
		}

		@Override
		public void run() {
			try {
				Thread.sleep(_overtime);
			} catch (InterruptedException e) {
				LogUtil.log("ObserveThread is interrupted!");
			} finally {
				if (!_isObservedFinished && _observed != null && _observed.isAlive() && !_observed.isInterrupted()) {
					_observed.interrupt();
				}
				_observed = null;
				removeWorkingInternal();
			}
		}
		
		/**
		 * 由工作线程结束执行前调用
		 */
		private void removeWorkingInternal() {
			mWorkingMap.remove(hashCode());
			mSemaphore.release();
			realExecute();
		}
	}

	static class ObservedThread extends Thread {
		private WeakReference<ObserverThread> _observerRef;

		public ObservedThread(Runnable target) {
			super(target);
		}

		public void setObserver(ObserverThread observer) {
			_observerRef = new WeakReference<ObserverThread>(observer);
		}

		@Override
		public void run() {
			super.run();
			if (_observerRef != null) {
				ObserverThread o = _observerRef.get();
				if (o != null && o.isAlive() && o.isInterrupted()) {
					o.setObservedFinished(true);
					o.interrupt();
					_observerRef.clear();
				}
			}
		}
	}
}
