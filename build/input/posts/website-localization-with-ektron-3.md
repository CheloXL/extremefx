Title: Website Localization with Ektron – Part 3
Published: 2010-09-12
Tags: Localization, Translation, Cms
Lead: Setting up correct Locales, adding Lang attributes and localizing hard-coded images are among the technical steps detailed in this final blog in our 3-part series on Website Localization with Ektron CMS.
---
Setting up correct Locales, adding Lang attributes and localizing hard-coded images are among the technical steps detailed in this final blog in our 3-part series on *Website Localization with Ektron CMS*. We covered an overview of CMS strategy issues in part 1, and gave more technical detail and steps in Part 2. In this final blog of the series we will show you how to solve some of the issues touched on in Part 2.

### Setting up the correct Locale

The first thing we should do is to make sure that the system responds to the user in the correct (user selected) locale. To do this, we should not only rely on the content returned by Ektron (that will be in the correct locale) but also we should tell the system to use that same locale to present any information requested outside Ektron in the same locale.

Since we are dealing with a website, and based on [Microsoft](http://www.microsoft.com/) best practices, the locale initialization must be done with the [InitializeCulture](https://docs.microsoft.com/en-us/previous-versions/bz9tc508(v=vs.140)) method, by overriding it and setting the `CurrentCulture` and `CurrentUICulture` for the current thread. The Culture value determines the results of culture-dependent functions, such as the date, number, and currency formatting, and so on. The UICulture value determines which resources are loaded for the web page.

Since we would have to use the above on every template on our website and that&#8217;s not only too time consuming, but also error prone (if you add a new web page and forget to add the code, some items on that web page may not work as expected), I usually create a LocalizablePage class that inherits from the `System.Web.UI.Page` and I override the `InitializeCulture` method with the initialization code. Then the only thing that I have to remember is to change the inheritance of every template from `System.Web.UI.Page` to `LocalizablePage`.

```csharp
namespace EktronLibrary {
	using System.Globalization;
	using System.Web.UI;

	public class LocalizablePage : Page {
		protected override void InitializeCulture() {
			//this is here just to make sure the culture is correctly initialized
			CultureInfo c = Utilities.CurrentCulture;
			base.InitializeCulture();
		}
	}
}
```

As you can see on the code above, I did not directly place all the initialization code on the `LocalizablePage` class, but I created a helper class that does all this for me. Also that class returns what is the actual user-selected locale and what&#8217;s the default locale selected in Ektron CMS. These two values will come in handy for some of the next tasks.

```csharp
namespace EktronLibrary {
	using System.Globalization;
	using System.Threading;
	using System.Web;
	using Ektron.Cms.Controls;

	public static class Utilities {
		private static int _lastCulture;
		private static CultureInfo _lastCultureInfo;
		private static readonly object _cultureLock = new object();

		static Utilities() {
			using (LanguageAPI languageApi = new LanguageAPI()) {
				DefaultCulture = new CultureInfo(languageApi.DefaultLanguageID);
			}
		}

		public static CultureInfo DefaultCulture {
			get;
			private set;
		}

		public static CultureInfo CurrentCulture {
			get {
				using(LanguageAPI languageApi = new LanguageAPI()) {
					if (_lastCulture != languageApi.CurrentLanguageID) {
						HttpContext.Current.Application.Lock();
						lock (_cultureLock) {
							_lastCulture = languageApi.CurrentLanguageID;
							_lastCultureInfo = new CultureInfo(_lastCulture);
							Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = _lastCultureInfo;
						}
						HttpContext.Current.Application.UnLock();
					}
				}
				return _lastCultureInfo;
			}
		}
	}
}
```

### Adding `LANG` attribute to the html tag

Now that we have our system and code properly set, we need to tell the browser/spiders in which language we are serving the page. For this you can:

* Add a Literal control inside the html tag and from the code behind, write the appropriate code for the literal to display the current locale, or
* Use the code below to simply replace the html tag by a .net control that will render the html tag and also take care of setting the correct attribute. As you can see, this piece of code uses the CurrentCulture property of our helper class to ensure that we always use the correct user selected locale.

```csharp
using System.Web.UI;

[assembly: TagPrefix(@"EktronLibrary.Controls", @"Ekt")]
namespace EktronLibrary.Controls {
	using System.Text;
	using System.Web.UI;

	[ToolboxData(@"<{0}:EktronHtmlTag runat=server />")]
	public class EktronHtmlTag : Control {
		protected override void Render(HtmlTextWriter pOutput) {
			StringBuilder strb = new StringBuilder(@"", Utilities.CurrentCulture.Name);
			pOutput.Write(strb.ToString());
			RenderChildren(pOutput);
			pOutput.Write("");
		}
	}
}
```

### Taking care of content directionality

Again, we created another control that you can simply drop in your templates (inside the body tag); making sure it wraps all the content of your website. This control will render a `DIV` tag with the `DIR` attribute correctly set (rtl for Right-to-Left or ltr for Left-to-Right) based on the page target language. The `DIR` attribute is being applied to a `DIV` and not to the `BODY` tag because some speakers of languages that use right-to-left scripts prefer the directionality of the user interface to be associated with the desktop environment and not with the content of a particular document.

```csharp
using System.Web.UI;

[assembly: TagPrefix(@"EktronLibrary.Controls", @"Ekt")]
namespace EktronLibrary.Controls {
	using System;
	using System.ComponentModel;
	using System.Text;
	using System.Web.UI;

	[ToolboxData(@"<{0}:EktronContentDirectionality runat=server />")]
	public class EktronContentDirectionality : Control {

		[Category("Ektron Component")]
		public string ClassName { get; set; }

		protected override void Render(HtmlTextWriter pOutput) {
			StringBuilder strb = new StringBuilder("<div ");
			string langName = Utilities.CurrentCulture.TwoLetterISOLanguageName;
			string dir = (langName.Equals(@"ar", StringComparison.OrdinalIgnoreCase) ||
			langName.Equals(@"ur", StringComparison.OrdinalIgnoreCase) ||
			langName.Equals(@"fa", StringComparison.OrdinalIgnoreCase) ||
			langName.Equals(@"he", StringComparison.OrdinalIgnoreCase)) ? @"rtl" : @"ltr";

			if (!string.IsNullOrEmpty(ClassName)) {
				strb.AppendFormat("class="{0}" ", ClassName);
			}

			strb.AppendFormat(@"dir=""{0}"">", dir);
			pOutput.Write(strb.ToString());
			RenderChildren(pOutput);
			pOutput.Write("");
		}
	}
}
```

### Localizing hard-coded images

If an image is linked on a page from the content in Ektron, it is easy to go to that page, edit the localized version and replace the image. But when dealing with images hard-coded into the templates (Logos, graphics with overprinted text, etc.) you cannot resort to the features Ektron provides. In these cases, you have to pick the correct image for the user selected locale.

The code below shows you an example of a user-control that takes care of everything for you. The control has the same set of attributes as the `IMG` tag. The only difference is on how the control renders the `IMG` tag. If the website is being displayed in the Ektron CMS default language (set on the web.config), the control will look for the image on the path/filename combination you specified on the `Source` attribute. But when the user changes the language of the site, the control will try to load the image from the same path as the original version, but the control will append to the name an underscore (_) and then the full name for the selected locale (*es-ES* for Spanish/Spain, *ja-JA* for Japanese/Japan, *zh-CN* for Simplified Chinese/China, etc). So, for example, if the original image is located under this path &#8220;/images/subfolder/header.jpg&#8221;, the Spanish version will be located/named as &#8220;/images/subfolder/header_es-ES.jpg&#8221; and the Japanese version will be &#8220;/images/subfolder/header_ja-JP.jpg&#8221;

```csharp
using System.Web.UI;

[assembly: TagPrefix(@"EktronLibrary.Controls", @"Ekt")]
namespace EktronLibrary.Controls {
	using System.Drawing.Design;
	using System.Web.UI;
	using System.ComponentModel;
	using System.Text;
	using System.Web;
	using System.Web.UI.Design;

	[ToolboxData(@"<{0}:EktronLocalizableImage runat=server />")]
	public class EktronLocalizableImage : Control {

		[Category("Ektron Component"), DefaultValue(""), Localizable(true)]
		public string Alt { get; set; }

		[Category("Ektron Component"), DefaultValue(""), Localizable(true)]
		public string Title { get; set; }

		[Category("Ektron Component"), DefaultValue("")]
		public string UseMap { get; set; }

		[Category("EktronComponent"), DefaultValue("")]
		public string ClassName { get; set; }

		[Category("EktronComponent"), DefaultValue("")]
		public string Style { get; set; }

		[Category("EktronComponent"), Editor(typeof (UrlEditor), typeof (UITypeEditor))]
		public string Source { get; set; }

		[Category("EktronComponent")]
		public int Width { get; set; }

		[Category("EktronComponent")]
		public int Height { get; set; }

		protected override void Render(HtmlTextWriter pOutput) {
			StringBuilder strb = new StringBuilder(@"<img");
			if (Utilities.CurrentCulture.LCID == Utilities.DefaultCulture.LCID) {
				strb.AppendFormat(@" src=""{0}""", Source);
			}
			else {
				int dot = Source.LastIndexOf('.');
				strb.AppendFormat(@" src=""{0}_{2}.{1}""", Source.Substring(0, dot), Source.Substring(dot + 1), Utilities.CurrentCulture.Name);
			}

			if (!string.IsNullOrEmpty(Alt)) {
				strb.AppendFormat(" alt="{0}"", HttpUtility.HtmlAttributeEncode(Alt));
			}

			if (!string.IsNullOrEmpty(Title)) {
				strb.AppendFormat(" title="{0}"", HttpUtility.HtmlAttributeEncode(Title));
			}

			if (!string.IsNullOrEmpty(UseMap)) {
				strb.AppendFormat(@" usemap=""{0}""", HttpUtility.HtmlAttributeEncode(UseMap));
			}

			if (!string.IsNullOrEmpty(ClassName)) {
				strb.AppendFormat(" class="{0}"", HttpUtility.HtmlAttributeEncode(ClassName));
			}

			if (!string.IsNullOrEmpty(Style)) {
				strb.AppendFormat(" style="{0}"", HttpUtility.HtmlAttributeEncode(Style));
			}

			if (Width > 0) {
				strb.AppendFormat(" width="{0}"", Width);
			}

			if (Height > 0) {
				strb.AppendFormat(" height="{0}"", Height);
			}

			strb.Append("/>");
			pOutput.Write(strb.ToString());
		}
	}
}
```

### Localizing hard-coded text

![Ektron Setup](/assets/images/EktronSmartForm.png)

You have two options here:

1. Remove all the hard-coded text from the templates, placing them in resource files (.resx) and then use the Literal asp.net control to place it back on the templates. The resource file will have to be sent to the translation company as part of the localization process, along with the content exported from Ektron. If you are using the LocalizablePage class as the base class for all your templates, the system will automatically pick-up the correct resource file once the translated resources are back in place.

2. Remove all the hard-coded text from the templates, placing them in smart-forms within Ektron, and then use the standard ContentBlock control from Ektron to place the text back into the templates. You will have to also provide the control with an XSLT file that the control will use to render the content, and the content ID for the text you want to display. This method has the advantages of letting the Website administrator edit/import/export the text from within Ektron&#8217;s Workarea.

```csharp
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output omit-xml-declaration="yes" />
	<xsl:template match="/root">
		<xsl:value-of select="DictionaryEntry"/>
	</xsl:template>
</xsl:stylesheet>
```

And there you have it… If you correctly implement the techniques mentioned above, you will have a &#8220;bullet-proof&#8221; base of templates that will be localization-friendly and that may require minimal adjustments to be done over the QA phase.

I hope the &#8220;Website Localization with Ektron CMS&#8221; 3-part series of articles has provided you with useful information on how to correctly author multilingual websites under the Ektron CMS.
