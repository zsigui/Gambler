package com.zzj.gambler.model;

import java.util.ArrayList;

public class RespData extends RespBase {

	public int pageCount;
	public DataGameCount gameCount;
	/**
	 * 按顺序值类型说明: <br />
	 * id、主队、客队、[主队id]、[客队id]、赛事名称、时间戳(ms)、
	 * [可否下注]
	 */
	public ArrayList<ArrayList<Object>> games;
	/**
	 * 暂时作用不打，不理会
	 */
	public ArrayList<String> headers;
}
