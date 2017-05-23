namespace Gambler.Utils.Interface
{
    public abstract class AbsVerifyCode : IVerifyCode
    {
        public abstract string ParseCode(byte[] imgBytes);


    }
}
