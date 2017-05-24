using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class HFRespBase<T>
    {
        public int code;
        public T data;
    }
}
