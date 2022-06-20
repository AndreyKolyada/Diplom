using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Diplom1.Helper
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper html, string src, string alt, string htmlAttributes = null)
        {
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", src);
            img.MergeAttribute("alt", alt);
            if (htmlAttributes != null)
            {
                img.MergeAttribute("class", htmlAttributes);
            }
            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
    }
}
