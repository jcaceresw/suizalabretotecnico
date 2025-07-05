using System.Web;

namespace web.Models
{
	public class ModalDialog
	{
		public string Name { get; set; }
		public string Title { get; set; }
		public bool IsLarge { get; set; }
		public Func<object, IHtmlString> HtmlContent { get; set; }
	}
}
