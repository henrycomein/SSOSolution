using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib
{
    public class HandleDataResult
    {
        /// <summary>
        /// 執行結果
        /// </summary>
        public int success { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 操作成功后腰跳转的url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 显示提示消息的dom(如 '#show','.item')
        /// </summary>
        public string target { get; set; }
        /// <summary>
        /// 额外数据
        /// </summary>
        public string addtionnalData { get; set; }
    }
}
