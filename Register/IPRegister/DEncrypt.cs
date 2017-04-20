//名称空间
using  System;
using  System.Security.Cryptography;
using  System.IO;
using  System.Text;

namespace IPRegister
{

    public class MD5_Crypto
    {
        /// <summary>   
        /// 使用MD5不可逆加密算法加密字符串返回加密后的字符串   
        /// </summary>  
        /// <param name="str">需要加密的字符串</param>   
        /// <returns>加密后的字符串</returns>   
        public static string MD5(string str)
        {
            byte[] bValue;
            byte[] bHash;
            string result = null;
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

            bValue = System.Text.Encoding.UTF8.GetBytes(str);

            bHash = MD5.ComputeHash(bValue);

            MD5.Clear();

            for (int i = 0; i < bHash.Length; i++)
            {
                if (bHash[i].ToString("x").Length == 1)
                {
                    //如果返回来是07这样的值，0会被省掉，所以强制加了一个0   
                    result += "0" + bHash[i].ToString("x");
                }
                else
                {
                    result += bHash[i].ToString("x");
                }
            }
            return result.ToUpper();
        }
    }      
    /// <summary>
    /// DES加解密
    /// </summary>
    public class DES_Crypto
    {
        //加密方法    
        public string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中                     
            //原来使用的UTF8编码，我改成Unicode编码了，不行              
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);                
            //建立加密对象的密钥和偏移量               
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法              
            //使得输入密码必须输入英文文本               
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write  the  byte  array  into  the  crypto  stream             
            //(It  will  end  up  in  the  memory  stream)               
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Get  the  data  back  from  the  memory  stream,  and  into  a  string              
            StringBuilder ret = new StringBuilder(); foreach (byte b in ms.ToArray())
            {
                //Format  as  hex                           
                ret.AppendFormat("{0:X2}", b);
            } 
            return ret.ToString();
        }    
        //解密方法    
        public string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //Put  the  input  string  into  the  byte  array               
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16)); 
                inputByteArray[x] = (byte)i;
            }
            //建立加密对象的密钥和偏移量，此值重要，不能修改              
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey); 
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey); 
            MemoryStream ms = new MemoryStream(); 
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream              
            cs.Write(inputByteArray, 0, inputByteArray.Length); 
            cs.FlushFinalBlock();
            
            //Get the decrypted data back from the memory stream
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象   
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());


        }
    }
}
