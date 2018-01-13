using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Visa.Service
{
    public class PagingBar
    {
        private static string GetUrl()
        {
            string ResultUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            ResultUrl = Regex.Replace(ResultUrl, @"page=(\d+)\&?", string.Empty, RegexOptions.IgnoreCase);
            if (ResultUrl.LastIndexOf("?") > 0)
            {
                if (ResultUrl.LastIndexOf("&") + 1 != ResultUrl.Length)
                {
                    if (ResultUrl.LastIndexOf("?") + 1 < ResultUrl.Length)
                        ResultUrl = ResultUrl + "&";
                }
            }
            else
                ResultUrl = ResultUrl + "?";
            return ResultUrl;
        }
        public static string CommonPage(int PageIndex, int ToatalCountRecord, int PageSize)
        {
            //目标
            int LeftSize = 3;

            string PageUrl = GetUrl() + "page={0}";
            int PageCount = (int)Math.Ceiling((double)(ToatalCountRecord) / PageSize);

            //需要输出的HTML
            StringBuilder html = new StringBuilder("<ul  class=\"pagination\">");

            html.Append("<li><a>共有" + ToatalCountRecord.ToString() + "条记录&nbsp;&nbsp;当前第" + PageIndex.ToString() + "/" + PageCount.ToString() + "页</a></li>");

            #region 输出上一页
            if (PageIndex <= 1)
            {
                html.Append("<li  class=\"disabled\"><a> < Prev </a></li>");
            }
            else
            {
                html.Append("<li><a href=\"" + string.Format(PageUrl, PageIndex - 1) + "\" > < Prev </a></li>");
            }
            #endregion

            //总长度
            int barSize = LeftSize * 2 + 1;

            #region 输出中间
            //页数小于0
            if (PageCount <= 0)
            {
                return "<ul  class=\"pagination\"><li><a>页数为0</a></li></ul>";
            }

            //总页数小于正常显示的条数 则全部显示出来
            if (PageCount <= barSize)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (PageIndex == i)
                    {
                        html.Append("<li class=\"active\"><a>");
                        html.Append(i);
                        html.Append("</a></li>");
                    }
                    else
                    {
                        html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                    }
                }
            }
            else
            {
                if (PageIndex < LeftSize + 1)
                {
                    for (int i = 1; i <= barSize; i++)
                    {
                        if (PageIndex == i)
                        {
                            html.Append("<li class=\"active\"><a>");
                            html.Append(i);
                            html.Append("</a></li>");
                        }
                        else
                        {
                            html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                        }
                    }

                    html.Append("<li><a href=\"" + string.Format(PageUrl, PageCount) + "\" >尾页</a></li>");
                }
                else
                {
                    html.Append("<li><a href=\"" + string.Format(PageUrl, 1) + "\" >首页</a></li>");
                    if (PageIndex < PageCount - LeftSize)
                    {
                        for (int i = PageIndex - LeftSize; i < PageIndex; i++)
                        {
                            if (i == 1)
                            {
                                continue;
                            }
                            html.Append("<li><a href=\"" + string.Format(PageUrl, i)).Append("\" >" + i + "</a></li>");
                        }
                        for (int i = PageIndex; i <= PageIndex + LeftSize; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<li class=\"active\"><a>");
                                html.Append(i);
                                html.Append("</a></li>");
                            }
                            else
                            {
                                html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                            }
                        }
                        html.Append("<li><a href=\"" + string.Format(PageUrl, PageCount)).Append("\" >尾页</a></li>");

                    }
                    else
                    {
                        for (int i = PageCount - LeftSize - LeftSize; i <= PageCount; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<li class=\"active\"><a>");
                                html.Append(i);
                                html.Append("</a></li>");
                            }
                            else
                            {
                                html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                            }
                        }
                    }
                }
            }
            #endregion

            #region 输出下一页
            if (PageIndex >= PageCount)
            {
                html.Append("<li class=\"disabled\"><a>Next></a></li>");
            }
            else
            {
                html.Append("<li><a href=\"" + string.Format(PageUrl, PageIndex + 1) + "\" > Next></a></li>");
            }
            #endregion
            html.Append("</ul>");
            return html.ToString();
        }
        public static string BuildPage(int PageIndex, int ToatalCountRecord, int PageSize)
        {
            //目标
            int LeftSize = 3;

            string PageUrl = GetUrl() + "page={0}";
            int PageCount = (int)Math.Ceiling((double)(ToatalCountRecord) / PageSize);

            //需要输出的HTML
            StringBuilder html = new StringBuilder("<div  class=\"column-page\">");

            //html.Append("<li><a>共有" + ToatalCountRecord.ToString() + "条记录&nbsp;&nbsp;当前第" + PageIndex.ToString() + "/" + PageCount.ToString() + "页</a></li>");

            #region 输出上一页
            if (PageIndex <= 1)
            {
                //html.Append("<a class=\"m-hide\">1</a>");
            }
            else
            {
                html.Append("<a class=\"prev\" href=\"" + string.Format(PageUrl, PageIndex - 1) + "\" > 上一页 </a>");
            }
            #endregion

            //总长度
            int barSize = LeftSize * 2 + 1;

            #region 输出中间
            //页数小于0
            if (PageCount <= 0)
            {
                return "<div class=\"column-page\"><a class=\"m-hide\">0</a></div>";
            }

            //总页数小于正常显示的条数 则全部显示出来
            if (PageCount <= barSize)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (PageIndex == i)
                    {
                        html.Append("<a class=\"m-hide on\">");
                        html.Append(i);
                        html.Append("</a>");
                    }
                    else
                    {
                        html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a>");
                    }
                }
            }
            else
            {
                if (PageIndex < LeftSize + 1)
                {
                    for (int i = 1; i <= barSize; i++)
                    {
                        if (PageIndex == i)
                        {
                            html.Append("<a class=\"m-hide on\">");
                            html.Append(i);
                            html.Append("</a>");
                        }
                        else
                        {
                            html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a>");
                        }
                    }

                    html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, PageCount) + "\" >尾页</a>");
                }
                else
                {
                    html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, 1) + "\" >首页</a>");
                    if (PageIndex < PageCount - LeftSize)
                    {
                        for (int i = PageIndex - LeftSize; i < PageIndex; i++)
                        {
                            if (i == 1)
                            {
                                continue;
                            }
                            html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, i)).Append("\" >" + i + "</a>");
                        }
                        for (int i = PageIndex; i <= PageIndex + LeftSize; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<a class=\"m-hide on\">");
                                html.Append(i);
                                html.Append("</a>");
                            }
                            else
                            {
                                html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a>");
                            }
                        }
                        html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, PageCount)).Append("\" >尾页</a>");

                    }
                    else
                    {
                        for (int i = PageCount - LeftSize - LeftSize; i <= PageCount; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<a class=\"m-hide on\">");
                                html.Append(i);
                                html.Append("</a>");
                            }
                            else
                            {
                                html.Append("<a class=\"m-hide\" href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a>");
                            }
                        }
                    }
                }
            }
            #endregion

            #region 输出下一页
            if (PageIndex >= PageCount)
            {
                html.Append("<a class=\"next\">下一页</a>");
            }
            else
            {
                html.Append("<a class=\"next\" href=\"" + string.Format(PageUrl, PageIndex + 1) + "\" > 下一页</a>");
            }
            #endregion
            html.Append("</div>");
            return html.ToString();
        }
        public static string BuildPage2(int PageIndex, int ToatalCountRecord, int PageSize)
        {
            //目标
            int LeftSize = 3;

            string PageUrl = GetUrl() + "page={0}";
            int PageCount = (int)Math.Ceiling((double)(ToatalCountRecord) / PageSize);

            //需要输出的HTML
            StringBuilder html = new StringBuilder("<ul  class=\"pagination\">");

            html.Append("<li><a>共有" + ToatalCountRecord.ToString() + "条记录&nbsp;&nbsp;当前第" + PageIndex.ToString() + "/" + PageCount.ToString() + "页</a></li>");

            #region 输出上一页
            if (PageIndex <= 1)
            {
                html.Append("<li  class=\"disabled\"><a> < Prev </a></li>");
            }
            else
            {
                html.Append("<li><a href=\"" + string.Format(PageUrl, PageIndex - 1) + "\" > < Prev </a></li>");
            }
            #endregion

            //总长度
            int barSize = LeftSize * 2 + 1;

            #region 输出中间
            //页数小于0
            if (PageCount <= 0)
            {
                return "<ul  class=\"pagination\"><li><a>页数为0</a></li></ul>";
            }

            //总页数小于正常显示的条数 则全部显示出来
            if (PageCount <= barSize)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (PageIndex == i)
                    {
                        html.Append("<li class=\"active\"><a>");
                        html.Append(i);
                        html.Append("</a></li>");
                    }
                    else
                    {
                        html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                    }
                }
            }
            else
            {
                if (PageIndex < LeftSize + 1)
                {
                    for (int i = 1; i <= barSize; i++)
                    {
                        if (PageIndex == i)
                        {
                            html.Append("<li class=\"active\"><a>");
                            html.Append(i);
                            html.Append("</a></li>");
                        }
                        else
                        {
                            html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                        }
                    }

                    html.Append("<li><a href=\"" + string.Format(PageUrl, PageCount) + "\" >尾页</a></li>");
                }
                else
                {
                    html.Append("<li><a href=\"" + string.Format(PageUrl, 1) + "\" >首页</a></li>");
                    if (PageIndex < PageCount - LeftSize)
                    {
                        for (int i = PageIndex - LeftSize; i < PageIndex; i++)
                        {
                            if (i == 1)
                            {
                                continue;
                            }
                            html.Append("<li><a href=\"" + string.Format(PageUrl, i)).Append("\" >" + i + "</a></li>");
                        }
                        for (int i = PageIndex; i <= PageIndex + LeftSize; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<li class=\"active\"><a>");
                                html.Append(i);
                                html.Append("</a></li>");
                            }
                            else
                            {
                                html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                            }
                        }
                        html.Append("<li><a href=\"" + string.Format(PageUrl, PageCount)).Append("\" >尾页</a></li>");

                    }
                    else
                    {
                        for (int i = PageCount - LeftSize - LeftSize; i <= PageCount; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<li class=\"active\"><a>");
                                html.Append(i);
                                html.Append("</a></li>");
                            }
                            else
                            {
                                html.Append("<li><a href=\"" + string.Format(PageUrl, i) + "\" >" + i + "</a></li>");
                            }
                        }
                    }
                }
            }
            #endregion

            #region 输出下一页
            if (PageIndex >= PageCount)
            {
                html.Append("<li class=\"disabled\"><a>Next></a></li>");
            }
            else
            {
                html.Append("<li><a href=\"" + string.Format(PageUrl, PageIndex + 1) + "\" > Next></a></li>");
            }
            #endregion
            html.Append("</ul>");
            return html.ToString();
        }
        public static string AjaxPage(int PageIndex, int ToatalCountRecord, int PageSize)
        {
            //目标
            int LeftSize = 3;

            string PageUrl = GetUrl() + "page={0}";
            int PageCount = (int)Math.Ceiling((double)(ToatalCountRecord) / PageSize);

            //需要输出的HTML
            StringBuilder html = new StringBuilder("<div  class=\"column-page\">");

            //html.Append("<li><a>共有" + ToatalCountRecord.ToString() + "条记录&nbsp;&nbsp;当前第" + PageIndex.ToString() + "/" + PageCount.ToString() + "页</a></li>");

            #region 输出上一页
            if (PageIndex <= 1)
            {
                //html.Append("<a class=\"m-hide\">1</a>");
            }
            else
            {
                html.Append("<a class=\"prev\" href=\"javascript:loadcomment(" + (PageIndex - 1).ToString() + ")\" > 上一页 </a>");
            }
            #endregion

            //总长度
            int barSize = LeftSize * 2 + 1;

            #region 输出中间
            //页数小于0
            if (PageCount <= 0)
            {
                return "<div class=\"column-page\"><a class=\"m-hide\">0</a></div>";
            }

            //总页数小于正常显示的条数 则全部显示出来
            if (PageCount <= barSize)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (PageIndex == i)
                    {
                        html.Append("<a class=\"m-hide on\">");
                        html.Append(i);
                        html.Append("</a>");
                    }
                    else
                    {
                        html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + i+ ")\">" + i + "</a>");
                    }
                }
            }
            else
            {
                if (PageIndex < LeftSize + 1)
                {
                    for (int i = 1; i <= barSize; i++)
                    {
                        if (PageIndex == i)
                        {
                            html.Append("<a class=\"m-hide on\">");
                            html.Append(i);
                            html.Append("</a>");
                        }
                        else
                        {
                            html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + i + ")\" >" + i + "</a>");
                        }
                    }

                    html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + PageCount + ")\" >尾页</a>");
                }
                else
                {
                    html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + 1 + ")\" >首页</a>");
                    if (PageIndex < PageCount - LeftSize)
                    {
                        for (int i = PageIndex - LeftSize; i < PageIndex; i++)
                        {
                            if (i == 1)
                            {
                                continue;
                            }
                            html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + i + ")\">" + i + "</a>");
                        }
                        for (int i = PageIndex; i <= PageIndex + LeftSize; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<a class=\"m-hide on\">");
                                html.Append(i);
                                html.Append("</a>");
                            }
                            else
                            {
                                html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + i + ")\" >" + i + "</a>");
                            }
                        }
                        html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + PageCount + ")\" >尾页</a>");

                    }
                    else
                    {
                        for (int i = PageCount - LeftSize - LeftSize; i <= PageCount; i++)
                        {
                            if (PageIndex == i)
                            {
                                html.Append("<a class=\"m-hide on\">");
                                html.Append(i);
                                html.Append("</a>");
                            }
                            else
                            {
                                html.Append("<a class=\"m-hide\" href=\"javascript:loadcomment(" + i + ")\" >" + i + "</a>");
                            }
                        }
                    }
                }
            }
            #endregion

            #region 输出下一页
            if (PageIndex >= PageCount)
            {
                html.Append("<a class=\"next\">下一页</a>");
            }
            else
            {
                html.Append("<a class=\"next\" href=\"javascript:loadcomment(" + (PageIndex+1).ToString()+ ")\" > 下一页</a>");
            }
            #endregion
            html.Append("</div>");
            return html.ToString();
        }
    }
}
