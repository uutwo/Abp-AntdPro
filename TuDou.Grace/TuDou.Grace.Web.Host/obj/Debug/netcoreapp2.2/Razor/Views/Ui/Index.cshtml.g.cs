#pragma checksum "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d58ded27d08ca2ab452f12ac78171708d59c8494"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ui_Index), @"mvc.1.0.view", @"/Views/Ui/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Ui/Index.cshtml", typeof(AspNetCore.Views_Ui_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\_ViewImports.cshtml"
using Abp.Localization;

#line default
#line hidden
#line 1 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
using TuDou.Grace.Authorization;

#line default
#line hidden
#line 2 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
using TuDou.Grace.Web.Common;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d58ded27d08ca2ab452f12ac78171708d59c8494", @"/Views/Ui/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d64c8de117175f3cc2a63fa65d8b618c3fa64035", @"/Views/_ViewImports.cshtml")]
    public class Views_Ui_Index : TuDou.Grace.Web.Views.GraceRazorPage<TuDou.Grace.Web.Models.Ui.HomePageModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/view-resources/Views/Ui/Index.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(110, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(111, 113, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d58ded27d08ca2ab452f12ac78171708d59c84944196", async() => {
                BeginContext(117, 31, true);
                WriteLiteral("\n    <title>Grace</title>\n\n    ");
                EndContext();
                BeginContext(148, 68, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d58ded27d08ca2ab452f12ac78171708d59c84944608", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(216, 1, true);
                WriteLiteral("\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(224, 55, true);
            WriteLiteral("\n<div class=\"main-content\">\n    <div class=\"user-name\">");
            EndContext();
            BeginContext(280, 34, false);
#line 11 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
                      Write(L("YouAreAlreadyLoggedInWithUser"));

#line default
#line hidden
            EndContext();
            BeginContext(314, 3, true);
            WriteLiteral(" : ");
            EndContext();
            BeginContext(318, 35, false);
#line 11 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
                                                            Write(Html.Raw(Model.GetShownLoginName()));

#line default
#line hidden
            EndContext();
            BeginContext(353, 51, true);
            WriteLiteral("</div>\n    <div>\n        <ul class=\"content-list\">\n");
            EndContext();
#line 14 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
             if (WebConsts.SwaggerUiEnabled)
            {

#line default
#line hidden
            BeginContext(463, 22, true);
            WriteLiteral("                <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 485, "\"", 520, 1);
#line 16 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
WriteAttributeValue("", 492, WebConsts.SwaggerUiEndPoint, 492, 28, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(521, 21, true);
            WriteLiteral(">Swagger UI</a></li>\n");
            EndContext();
#line 17 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(556, 12, true);
            WriteLiteral("            ");
            EndContext();
#line 18 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
             if (WebConsts.HangfireDashboardEnabled && IsGranted(AppPermissions.Pages_Administration_HangfireDashboard))
            {

#line default
#line hidden
            BeginContext(691, 22, true);
            WriteLiteral("                <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 713, "\"", 756, 1);
#line 20 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
WriteAttributeValue("", 720, WebConsts.HangfireDashboardEndPoint, 720, 36, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(757, 19, true);
            WriteLiteral(">Hangfire</a></li>\n");
            EndContext();
#line 21 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(790, 12, true);
            WriteLiteral("            ");
            EndContext();
#line 22 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
             if (WebConsts.GraphQL.Enabled && WebConsts.GraphQL.PlaygroundEnabled)
            {

#line default
#line hidden
            BeginContext(887, 22, true);
            WriteLiteral("                <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 909, "\"", 953, 1);
#line 24 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
WriteAttributeValue("", 916, WebConsts.GraphQL.PlaygroundEndPoint, 916, 37, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(954, 29, true);
            WriteLiteral(">GraphQL Playground</a></li>\n");
            EndContext();
#line 25 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(997, 60, true);
            WriteLiteral("        </ul>\n    </div>\n    <div class=\"logout\">\n        <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1057, "\"", 1118, 1);
#line 29 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
WriteAttributeValue("", 1064, Url.Action("Logout", "Ui", new {area = string.Empty}), 1064, 54, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1119, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1121, 11, false);
#line 29 "D:\个人程序文件\个人项目\TuDou.Grace\TuDou.Grace.Web.Host\Views\Ui\Index.cshtml"
                                                                    Write(L("Logout"));

#line default
#line hidden
            EndContext();
            BeginContext(1132, 22, true);
            WriteLiteral("</a>\n    </div>\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TuDou.Grace.Web.Models.Ui.HomePageModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
