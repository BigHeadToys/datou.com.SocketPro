using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datou.com.SocketPro
{
    public class JSONHelper
    {
        /// <summary>
        /// 实体对象转换成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string EntityToJson<T>(T x)
        { 
            string result = string.Empty;
            try
            {
                result = JsonConvert.SerializeObject(x);
            }
            catch (Exception)
            {

               //do nothing
            }
            return result;
        }

        /// <summary>
        /// Json字符串转换成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToEntity<T>(string json)
        { 
            T t = default(T);
            try
            {
                t = (T)JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                //do nothing
            }
            return t;
            
        }
        
    }
}
