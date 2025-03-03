using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LeQuangTrungMVC.TagHelpers
{
    [HtmlTargetElement("delete-button")]
    public class DeleteButtonTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public DeleteButtonTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [HtmlAttributeName("controller-name")]
        public string ControllerName { get; set; }

        [HtmlAttributeName("action-name")]
        public string ActionName { get; set; } = "Delete";

        [HtmlAttributeName("item-id")]
        public string ItemId { get; set; }

        [HtmlAttributeName("item-name")]
        public string? ItemName { get; set; }

        [HtmlAttributeName("item-type")]
        public string? ItemType { get; set; } = "item";

        [HtmlAttributeName("button-class")]
        public string ButtonClass { get; set; } = "btn btn-sm btn-danger";

        [HtmlAttributeName("button-text")]
        public string ButtonText { get; set; } = "Delete";

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var deleteUrl = urlHelper.Action(ActionName, ControllerName, new { id = ItemId });

            output.TagName = "button";
            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("class", ButtonClass);

            // Fix the data attribute names to match what the JS expects
            output.Attributes.SetAttribute("data-delete-url", deleteUrl);
            output.Attributes.SetAttribute("data-delete-name", ItemName ?? "");
            output.Attributes.SetAttribute("data-delete-type", ItemType);

            output.Content.SetContent(ButtonText);
        }
    }
}
