#pragma checksum "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "634504a961ad5ebbb9c323270d35b0b5fb9f6268"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_MoveSuccess), @"mvc.1.0.view", @"/Views/Employee/MoveSuccess.cshtml")]
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
#nullable restore
#line 1 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\_ViewImports.cshtml"
using Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"634504a961ad5ebbb9c323270d35b0b5fb9f6268", @"/Views/Employee/MoveSuccess.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"74b0619e1a302f0598271da1847e697c39d57b88", @"/Views/_ViewImports.cshtml")]
    public class Views_Employee_MoveSuccess : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Web.Models.Employee.EmployeeIndexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Equipment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml"
  
    ViewBag.Title = "Оборудование";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row mb-5 mt-5"">
    <div class=""col-12 shadow-lg p-0 bg-white"">
        <div style=""width: 130%; margin-left: -15%;"" class=""card"">
            <div class=""card-header"">
                <h3>Оборудование</h3>
            </div>
            <div class=""card-body"">
                <div>
                    <h2 class=""text-success"">Оборудование успешно перемещено</h2>
                    <p>Название: ");
#nullable restore
#line 16 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml"
                            Write(ViewBag.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p>Инвентарный номер: ");
#nullable restore
#line 17 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml"
                                     Write(ViewBag.InventoryNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p>С отдела: ");
#nullable restore
#line 18 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml"
                            Write(ViewBag.OldDepartment);

#line default
#line hidden
#nullable disable
            WriteLiteral(" в отдел: ");
#nullable restore
#line 18 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml"
                                                            Write(ViewBag.NewDepartment);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p>Ответственный работник был: ");
#nullable restore
#line 19 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml"
                                              Write(ViewBag.OldEmployeeFullName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" стал: ");
#nullable restore
#line 19 "C:\Users\asus\Desktop\EquipmentAccounting\scr\Web\Views\Employee\MoveSuccess.cshtml"
                                                                                 Write(ViewBag.NewEmployeeFullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "634504a961ad5ebbb9c323270d35b0b5fb9f62686598", async() => {
                WriteLiteral("OK");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Web.Models.Employee.EmployeeIndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
