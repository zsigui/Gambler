package com.zzj.gambler.test;

import java.util.ArrayList;

import com.zzj.gambler.model.ReqBetData;
import com.zzj.gambler.model.ReqBetItem;
import com.zzj.gambler.model.RespBase;
import com.zzj.gambler.model.RespBet;
import com.zzj.gambler.model.RespData;
import com.zzj.gambler.model.RespLeague;
import com.zzj.gambler.model.RespLogin;
import com.zzj.gambler.xinpujin.XPJClient;
import com.zzj.gambler.xinpujin.XPJClient.RespCallback;
import com.zzj.gambler.xinpujin.XPJHelper;

public class HttpTest {

	XPJClient client;
	
	public static void main(String[] args) {
		new HttpTest().work();
	}
	
	private void work() {
		client = new XPJClient("smile", "cs006366129");
		client.login(new DefaultCallback<RespLogin>() {

			@Override
			public void onSuccess(RespLogin data) {
				System.out.println("当前登录状态: " + data.success);
				doAfterLoginSuccess();
			}
		});
	}

	protected void doAfterLoginSuccess() {
		client.requestLeagueData(XPJClient.GameType.FT_RB_MN, new DefaultCallback<RespLeague>() {

			@Override
			public void onSuccess(RespLeague data) {
				System.out.println(data.leagues);
				// 查找特定联盟赛
				final String league = "保加利亚甲组联赛-附加赛";
				final String hName = "PFC蒙大拿";
				final String cName = "内夫托西米克";
				client.requestOddData(XPJClient.GameType.FT_RB_MN, 1, 1, new String[]{league},
						new DefaultCallback<RespData>() {

							@Override
							public void onSuccess(RespData data) {
								ArrayList<String> item = XPJClient.searchMatch(data, hName, cName, league);
								if (item != null) {
									// 模拟下注，大
									int i = XPJClient.searchIorIndex(data.headers, "ior_OUH");
									int si = XPJClient.searchIorIndex(data.headers, "ior_OUC");
									if (i != -1) {
										ReqBetData bet = new ReqBetData();
										bet.gameType = XPJClient.GameType.FT_RB_MN;
										bet.acceptBestOdds = true;
										bet.money = String.valueOf(50.00f);
										bet.items = new ArrayList<>();
										ReqBetItem reqItem = new ReqBetItem();
										reqItem.gid = Integer.parseInt(item.get(0));
										reqItem.odds = new XPJHelper().getIor("H", item.get(i), item.get(si))[0];
										reqItem.scoreC = item.get(9);
										reqItem.scoreH = item.get(10);
										reqItem.project = item.get(i + 1);
										reqItem.type = "ior_OUH";
										bet.items.add(reqItem);
										
										client.requestBet(bet, new DefaultCallback<RespBet>() {

											@Override
											public void onSuccess(RespBet data) {
												System.out.println(data.newOdds + ", " + data.code);
											}
										});
									} else {
										System.out.println("找不到 ior_OUH 这个项");
									}
								} else {
									System.out.println("找不到指定的比赛");
								}
							}
						});
			}
		});
		client.requestOddData(XPJClient.GameType.FT_RB_MN, new DefaultCallback<RespData>() {

			@Override
			public void onSuccess(RespData data) {
				if (data.games.isEmpty()) return;
				
				ArrayList<String> item = data.games.get(0);
				System.out.println(item.get(0) + " : " + item.get(1) + " vs " + item.get(2) + ", " + item.get(5));
			}
		});
	}
	
	public static abstract class DefaultCallback<T extends RespBase> implements RespCallback<T> {

		@Override
		public void onFailed(int httpStatus, String errorMsg) {
			System.out.println("code = " + httpStatus + ", err = " + errorMsg);
		}

		@Override
		public void onError(Throwable t) {
			
		}
		
	}
}
