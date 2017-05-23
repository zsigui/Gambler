using System;

namespace Gambler.Utils.Interface
{
    public interface IVerifyCode
    {
        String ParseCode(byte[] imgBytes);
    }
}
