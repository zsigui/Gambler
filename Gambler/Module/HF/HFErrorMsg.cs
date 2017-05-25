using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF
{
    public class HFErrorMsg : BaseError
    {
        #region 错误码说明
        /*
         {
            "success_to_login": {
                "code": 201001, 
                "message": "登录成功"
            }, 
            "fail_to_login": {
                "code": 211002, 
                "message": "登录失败，请稍后再试"
            }, 
            "fail_to_login_error": {
                "code": 211003, 
                "message": "用户名或密码错误"
            }, 
            "is_have_username": {
                "code": 211004, 
                "message": "该用户名已经存在"
            }, 
            "is_have_no_username": {
                "code": 201005, 
                "message": "该用户名可以使用"
            }, 
            "is_have_truename": {
                "code": 211006, 
                "message": "该真实姓名已经存在"
            }, 
            "is_have_no_truename": {
                "code": 201007, 
                "message": "该真实姓名可以使用"
            }, 
            "is_have_mobile": {
                "code": 211008, 
                "message": "该手机号码已经存在"
            }, 
            "is_have_no_mobile": {
                "code": 201009, 
                "message": "该手机号码可以使用"
            }, 
            "is_login_status": {
                "code": 201013, 
                "message": "登录状态"
            }, 
            "is_not_login_status": {
                "code": 211014, 
                "message": "用户已经登出"
            }, 
            "success_to_change_pwd": {
                "code": 201015, 
                "message": "修改密码成功"
            }, 
            "fail_to_change_pwd": {
                "code": 211016, 
                "message": "修改密码失败，请联系客服"
            }, 
            "fail_to_change_pwd_error": {
                "code": 211017, 
                "message": "修改密码失败，原密码错误"
            }, 
            "success_to_reg_and_login": {
                "code": 201018, 
                "message": "用户注册并且登录成功"
            }, 
            "success_to_reg": {
                "code": 201019, 
                "message": "用户注册成功，请返回登录"
            }, 
            "fail_to_reg": {
                "code": 211020, 
                "message": "用户注册失败，请稍后再试"
            }, 
            "fail_to_agentreg": {
                "code": 211021, 
                "message": "代理注册失败，请稍后再试"
            }, 
            "success_to_agentreg": {
                "code": 201022, 
                "message": "代理注册成功，请联系客服审核通知"
            }, 
            "success_check_password": {
                "code": 201023, 
                "message": "原密码正确"
            }, 
            "fail_check_password": {
                "code": 211024, 
                "message": "原密码不正确，请重新输入"
            }, 
            "success_get_getmoney_userdetial": {
                "code": 201025, 
                "message": "获取用户出款资料成功"
            }, 
            "fail_get_getmoney_userdetial": {
                "code": 211026, 
                "message": "获取用户出款资料失败"
            }, 
            "success_connect_bank": {
                "code": 201027, 
                "message": "绑定出款银行信息成功"
            }, 
            "fail_connect_bank": {
                "code": 211028, 
                "message": "绑定出款银行信息失败"
            }, 
            "fail_to_login_stopuse": {
                "code": 211029, 
                "message": "该用户已被停用"
            }, 
            "fail_to_login_error_code": {
                "code": 211030, 
                "message": "验证码错误"
            }, 
            "success_get_user_detail": {
                "code": 201031, 
                "message": "获取用户详细资料成功"
            }, 
            "fail_get_user_detail": {
                "code": 211032, 
                "message": "获取用户详细资料失败"
            }, 
            "success_up_user_detail": {
                "code": 201033, 
                "message": "修改用户详细资料成功"
            }, 
            "fail_up_user_detail": {
                "code": 211034, 
                "message": "修改用户详细资料失败"
            }, 
            "success_get_user_point": {
                "code": 211037, 
                "message": "获取用户积分成功"
            }, 
            "fail_get_user_point": {
                "code": 211038, 
                "message": "获取用户积分失败"
            }, 
            "fail_to_login_black": {
                "code": 211039, 
                "message": "用户名或密码错误"
            }, 
            "is_have_email": {
                "code": 211040, 
                "message": "该电子邮箱已经存在"
            }, 
            "is_have_no_email": {
                "code": 201041, 
                "message": "该电子邮箱可以使用"
            }, 
            "is_have_qq": {
                "code": 211042, 
                "message": "该QQ已经存在"
            }, 
            "is_have_no_qq": {
                "code": 201043, 
                "message": "该QQ可以使用"
            }, 
            "is_have_bankaccount": {
                "code": 211044, 
                "message": "该银行账号已经存在"
            }, 
            "is_have_no_bankaccount": {
                "code": 201045, 
                "message": "该银行账号可以使用"
            }, 
            "fail_to_due_black": {
                "code": 211046, 
                "message": "用户异常"
            }, 
            "fail_to_reg_too_many_ips": {
                "code": 211035, 
                "message": "注册失败，同一IP注册账号过多！"
            }, 
            "fail_to_reg_not_notallow": {
                "code": 211036, 
                "message": "注册功能已关闭！"
            }, 
            "check_username_length_error": {
                "code": 211211, 
                "message": "用户名必须是4-12位之间的字母和数字"
            }, 
            "check_password_length_error": {
                "code": 211212, 
                "message": "登录密码必须是8-12位之间的字母和数字"
            }, 
            "check_password_two_error": {
                "code": 211213, 
                "message": "确认密码必须一样"
            }, 
            "check_username_must_error": {
                "code": 211214, 
                "message": "用户名必须填写"
            }, 
            "check_truename_must_error": {
                "code": 211215, 
                "message": "真实姓名必须填写"
            }, 
            "check_mobile_must_error": {
                "code": 211216, 
                "message": "手机号码必须填写"
            }, 
            "check_password_must_error": {
                "code": 211217, 
                "message": "密码必须填写"
            }, 
            "check_mobile_style_error": {
                "code": 211218, 
                "message": "请输入正确的手机号码"
            }, 
            "check_agree_error": {
                "code": 211219, 
                "message": "未满18岁请不要进入游戏"
            }, 
            "check_bankname_must_error": {
                "code": 211220, 
                "message": "银行名称必须选择"
            }, 
            "check_bankaccount_must_error": {
                "code": 211221, 
                "message": "银行账号必须填写"
            }, 
            "check_bankaddress_must_error": {
                "code": 211222, 
                "message": "银行所属地址必须填写"
            }, 
            "check_email_error": {
                "code": 211224, 
                "message": "电子邮件格式不对"
            }, 
            "check_qq_error": {
                "code": 211225, 
                "message": "QQ号码必须是数字"
            }, 
            "check_truename_must_ch_en": {
                "code": 211226, 
                "message": "真实姓名必须是中文或者英文小写字母"
            }, 
            "check_truename_length_error": {
                "code": 211226, 
                "message": "请输入正确格式的真实姓名"
            }, 
            "success_get_notice": {
                "code": 302001, 
                "message": "获取公告成功"
            }, 
            "fail_get_notice": {
                "code": 312002, 
                "message": "获取公告失败"
            }, 
            "success_get_history_notice": {
                "code": 302003, 
                "message": "获取历史公告成功"
            }, 
            "fail_get_history_notice": {
                "code": 312004, 
                "message": "获取历史公告失败"
            }, 
            "success_get_weihu": {
                "code": 302005, 
                "message": "获取维护信息成功"
            }, 
            "fail_get_weihu": {
                "code": 312006, 
                "message": "获取维护信息失败"
            }, 
            "success_get_message": {
                "code": 302007, 
                "message": "获取短信息成功"
            }, 
            "fail_get_message": {
                "code": 312008, 
                "message": "获取短信息失败"
            }, 
            "success_get_game_type": {
                "code": 302009, 
                "message": "获取游戏类型成功"
            }, 
            "fail_get_game_type": {
                "code": 312010, 
                "message": "获取游戏类型失败"
            }, 
            "error_vcode": {
                "code": 312011, 
                "message": "验证码错误"
            }, 
            "success_get_img_con": {
                "code": 302012, 
                "message": "获取图片站信息成功"
            }, 
            "fail_get_img_con": {
                "code": 312013, 
                "message": "获取图片站信息失败"
            }, 
            "success_get_limit": {
                "code": 302014, 
                "message": "获取限额信息成功"
            }, 
            "fail_get_limit": {
                "code": 312015, 
                "message": "获取限额信息失败"
            }, 
            "success_to_get_main_money": {
                "code": 103001, 
                "message": "获取KG主账户成功"
            }, 
            "success_to_get_ag_money": {
                "code": 103121, 
                "message": "获取AG余额成功"
            }, 
            "success_to_get_bin_money": {
                "code": 103122, 
                "message": "获取BBIN余额成功"
            }, 
            "success_to_get_h8_money": {
                "code": 103123, 
                "message": "获取H8余额成功"
            }, 
            "success_to_get_mg_money": {
                "code": 103124, 
                "message": "获取MG余额成功"
            }, 
            "fail_to_get_main_money": {
                "code": 113124, 
                "message": "获取KG主账户失败"
            }, 
            "fail_to_get_ag_money": {
                "code": 113125, 
                "message": "获取AG余额失败"
            }, 
            "fail_to_get_bin_money": {
                "code": 113126, 
                "message": "获取BBIN余额失败"
            }, 
            "fail_to_get_h8_money": {
                "code": 113127, 
                "message": "获取H8余额失败"
            }, 
            "fail_to_get_mg_money": {
                "code": 113128, 
                "message": "获取MG余额失败"
            }, 
            "success_save_online": {
                "code": 103005, 
                "message": "在线存款成功"
            }, 
            "fail_save_online": {
                "code": 113006, 
                "message": "在线存款订单提交失败，请联系客服"
            }, 
            "success_save_onbank": {
                "code": 103007, 
                "message": "你的存款申请已成功提交，正在等待系统处理，请注意查看钱包余额！"
            }, 
            "fail_save_onbank": {
                "code": 113008, 
                "message": "公司存款订单提交失败"
            }, 
            "success_get_banks": {
                "code": 103009, 
                "message": "获取所有银行信息成功"
            }, 
            "fail_get_banks": {
                "code": 113010, 
                "message": "获取所有银行信息失败"
            }, 
            "get_prizes": {
                "code": 103011, 
                "message": "获取奖金列表数据"
            }, 
            "post_bill_copy": {
                "code": 113017, 
                "message": "请不要重复提交订单"
            }, 
            "post_bill_more": {
                "code": 113018, 
                "message": "频繁提交订单，请稍后再试"
            }, 
            "success_change_money": {
                "code": 103019, 
                "message": "转账成功"
            }, 
            "fail_change_money": {
                "code": 113020, 
                "message": "转账失败，请稍后再试"
            }, 
            "unknow_change_money": {
                "code": 113021, 
                "message": "转账处理中，请稍后查看"
            }, 
            "success_get_bank_set": {
                "code": 103022, 
                "message": "获取收款银行信息成功"
            }, 
            "fail_get_bank_set": {
                "code": 113023, 
                "message": "没有收款银行，请联系客服"
            }, 
            "success_get_billno": {
                "code": 103024, 
                "message": "生成订单号成功"
            }, 
            "fail_get_billno": {
                "code": 113025, 
                "message": "生成订单号失败"
            }, 
            "error_finger": {
                "code": 113026, 
                "message": "指纹不正确，数据异常"
            }, 
            "success_change_onlineorder": {
                "code": 103027, 
                "message": "修改在线存款订单成功"
            }, 
            "fail_change_onlineorder": {
                "code": 113028, 
                "message": "修改在线存款订单失败"
            }, 
            "success_get_saveonline_banks": {
                "code": 103029, 
                "message": "获取在线存款支持银行成功"
            }, 
            "fail_get_saveonline_banks": {
                "code": 113030, 
                "message": "获取在线存款支持银行失败"
            }, 
            "success_add_check": {
                "code": 103037, 
                "message": "添加稽核成功"
            }, 
            "fail_add_check": {
                "code": 113038, 
                "message": "添加稽核失败"
            }, 
            "success_get_record": {
                "code": 900000, 
                "message": "获取报表成功"
            }, 
            "success_take_bonus": {
                "code": 103039, 
                "message": "提取奖金成功"
            }, 
            "fail_take_bonus": {
                "code": 113040, 
                "message": "提取奖金失败"
            }, 
            "success_get_checklist": {
                "code": 103041, 
                "message": "获取稽核成功"
            }, 
            "fail_get_checklist": {
                "code": 113042, 
                "message": "获取稽核失败,请联系客服"
            }, 
            "success_get_online_set": {
                "code": 103043, 
                "message": "获取线上存款设置成功"
            }, 
            "success_fail_online_set": {
                "code": 113044, 
                "message": "获取线上存款设置失败"
            }, 
            "success_get_money_logs": {
                "code": 103045, 
                "message": "获取现金流日志成功"
            }, 
            "fail_get_money_logs": {
                "code": 113046, 
                "message": "获取现金流日志失败"
            }, 
            "success_take_money_order": {
                "code": 103047, 
                "message": "添加出款订单成功 "
            }, 
            "fail_take_money_order": {
                "code": 113048, 
                "message": "添加出款订单失败"
            }, 
            "success_change_getpassword": {
                "code": 103049, 
                "message": "修改出款密码成功 "
            }, 
            "fail_change_getpassword": {
                "code": 113050, 
                "message": "修改出款密码失败，请联系客服"
            }, 
            "error_getpassword": {
                "code": 113051, 
                "message": "出款密码错误"
            }, 
            "fail_save_more_than_limit": {
                "code": 113052, 
                "message": "存款已经超出最高限额"
            }, 
            "fail_save_less_than_limit": {
                "code": 113053, 
                "message": "存款不能低于最低限额"
            }, 
            "success_get_spread_promos": {
                "code": 103054, 
                "message": "获取推广红利成功 "
            }, 
            "fail_get_spread_promos": {
                "code": 113055, 
                "message": "获取推广红利失败"
            }, 
            "is_not_allow_save": {
                "code": 113056, 
                "message": "您存/取款操作已被停用，请联系客服"
            }, 
            "exerror": {
                "code": 113058, 
                "message": "没有支付配置"
            }
        }
         */
        #endregion

        private static HashSet<int> SET_SUCC = new HashSet<int>()
        {
            103001, // 获取KG主账户成功
            103121, // 获取AG余额成功
            103122, // 获取BBIN余额成功
            103123, // 获取H8余额成功
            103124, // 获取MG余额成功
            201001, // 登录成功
            211037, // 获取用户积分成功
            201013, // 处于登录中
        };

        public static int I_LOGOUT = 211014;

        private static Dictionary<int, string> DICT_FAIL = new Dictionary<int, string>()
        {
            {211002, "登录失败，请稍后再试"},
            {211003, "用户名或密码错误"},
            {113124, "获取KG主账户失败"},
            {113125, "获取AG余额失败"},
            {113126, "获取BBIN余额失败"},
            {113127, "获取H8余额失败"},
            {113128, "获取MG余额失败"},
            {211014, "用户已经登出"},
            {211030, "验证码错误"}
        };

        internal static bool IsSuccess(int code)
        {
            return SET_SUCC.Contains(code);
        }

        internal static string GetMessageByCode(int code)
        {
            return DICT_FAIL[code];
        }
    }
}
