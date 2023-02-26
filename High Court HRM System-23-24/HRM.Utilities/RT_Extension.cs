using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Utilities
{
    public static class Extensions
    {
        public static void SetStringX(this ISession session, string key, string value)
        {
            session.SetString(key, value != null ? value : string.Empty);
        }

        public static string GetStringX(this ISession session, string key)
        {
            string s = session.GetString(key);
            if (string.IsNullOrEmpty(s))
                return null;
            return s;
        }
        
        public static IHtmlContent DisplayImage(this IHtmlHelper html, string? src, string cssClass = "img", string alt = "NoImage")
        {
            //Type type = metadata.ModelType;
            string url = "images/noImage.png";
            if (src != null)
                url = src.ToString();
            url = url.Replace('\\', '/');
            StringBuilder sb = new StringBuilder();
            //var result = ((LambdaExpression)expression).Compile().DynamicInvoke(html.ViewData.Model);
            
                    //if (attribute is DisplayAttribute)
                    {
                        //DisplayAttribute d = attribute as DisplayAttribute;
                        //var displayName = d.Name;
                        sb.Append("<div class=\"form-group\">");
                        //  sb.AppendFormat("<label for=\"{0}\">{1}</label>", propName, displayName);
                        sb.AppendFormat(
                                 //"<input type=\"email\" class=\"form-control\" id=\"{0}\" placeholder=\"Enter email\">",
                                 "<img  id=\"_{0}\" src=\"/{2}\" class=\"{1} customImage\" onclick='customImageAction(\"_{0}\")'/>",
                                 "d"+(new Random().Next(1,1000)), cssClass, url.Replace('\\', '/'));
                        sb.Append("</div>");
                        return new HtmlContentBuilder().AppendHtml(sb.ToString());
                    }
               
            return new HtmlContentBuilder().Append("");
        }
        public static IHtmlContent DisplayForImage<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string? src,string cssClass = "img", string alt = "NoImage")
        {
            ExpressionType type = expression.Body.NodeType;

            //var t = new List<TModel>() { html.Value()}
            var t = html.Value(expression.ToString()).FirstOrDefault();

            // Get the property name
            //string name = ExpressionHelper.GetExpressionText(expression);
            //// Get the property type
            //Type type = metadata.ModelType;
            string url = "images/noImage.png";
            if (src != null)
                url = src.ToString();
            url = url.Replace('\\', '/');
            var compiletedExpress = expression.Compile();
            
            //var result = ((LambdaExpression)expression).Compile().DynamicInvoke(html.ViewData.Model);
            if (type == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)expression.Body;
                var propName = memberExpression.Member.Name;

                var member = memberExpression.Member as PropertyInfo;

                var attributes = member.GetCustomAttributes();

                StringBuilder sb = new StringBuilder();
                //foreach (var attribute in attributes)
                {
                    //if (attribute is DisplayAttribute)
                    {
                        //DisplayAttribute d = attribute as DisplayAttribute;
                        //var displayName = d.Name;
                        sb.Append("<div class=\"form-group\">");
                      //  sb.AppendFormat("<label for=\"{0}\">{1}</label>", propName, displayName);
                        sb.AppendFormat(
                                 //"<input type=\"email\" class=\"form-control\" id=\"{0}\" placeholder=\"Enter email\">",
                                 "<img  id=\"_{0}\" src=\"/{2}\" class=\"{1} customImage\" onclick='customImageAction(\"_{0}\")'/>",
                                 propName.ToUpper(),cssClass, url.Replace('\\', '/'));
                        sb.Append("</div>");
                        return new HtmlContentBuilder().AppendHtml(sb.ToString());
                    }
                }

            }
            return new HtmlContentBuilder().Append("");
        }
        public static IHtmlContent EditorForImage<TModel, TResult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression,string src, string cssClass = "img", string alt = "NoImage")
        {
            ExpressionType type = expression.Body.NodeType;

            //var t = new List<TModel>() { html.Value()}
            var t = html.Value(expression.ToString()).FirstOrDefault();

            // Get the property name
            //string name = ExpressionHelper.GetExpressionText(expression);
            //// Get the property type
            //Type type = metadata.ModelType;

            string url = "images/noImage.png";
            if (src != null)
                url = src.ToString();
            
            if (type == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)expression.Body;
                var propName = memberExpression.Member.Name;

                var member = memberExpression.Member as PropertyInfo;

                var attributes = member.GetCustomAttributes();

                StringBuilder sb = new StringBuilder();
                //foreach (var attribute in attributes)
                //{
                //    if (attribute is DisplayAttribute)
                //    {

                //    }
                //}

                //DisplayAttribute d = attribute as DisplayAttribute;
                //var displayName = d.Name;

                //  sb.AppendFormat("<label for=\"{0}\">{1}</label>", propName, displayName);
                sb.AppendFormat(
                         "<div class='customImageDiv'><img  id=\"_image_{0}\" src=\"/{2}\" class=\"{1} customImage\" onclick='customImageAction(\"_{0}\")' />",
                         propName.ToUpper(), cssClass, url.Replace('\\', '/'));
                sb.AppendFormat("<input name=\"_{0}\" type='file' class='customImageUpload' id='_{0}' />", propName.ToUpper());

                sb.AppendFormat("<input name=\"{0}\" type='hidden' value='{1}' id='_input{2}' /></div>", propName, url, propName.ToUpper());
                return new HtmlContentBuilder().AppendHtml(sb.ToString());

            }
            return new HtmlContentBuilder().Append("");
        }
        //public object GetValue<T>(Expression<Func<T>> accessor)
        //{
        //    var func = accessor.Compile();

        //    return func.Invoke();
        //}
    }
}
