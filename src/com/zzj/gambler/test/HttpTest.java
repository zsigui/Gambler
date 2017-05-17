package com.zzj.gambler.test;

import java.io.File;

import com.zzj.gambler.model.RespLeague;
import com.zzj.gambler.model.RespLogin;
import com.zzj.gambler.utils.ThreadUtil;
import com.zzj.gambler.xinpujin.XPJClient;
import com.zzj.gambler.xinpujin.XPJClient.RespCallback;

public class HttpTest {

	public static void main(String[] args) {
		XPJClient client = new XPJClient("smile", "cs006366129");
		client.login(null);
		client.requestLeagueData("FT_TD_MN", new RespCallback<RespLeague>() {

			@Override
			public void onSuccess(RespLeague data) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void onFailed(int httpStatus, String errorMsg) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void onError(Throwable t) {
				// TODO Auto-generated method stub
				
			}
		});
	}
}
