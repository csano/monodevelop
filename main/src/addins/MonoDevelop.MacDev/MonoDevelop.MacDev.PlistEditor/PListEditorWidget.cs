// 
// PListEditorWidget.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2011 Xamarin <http://xamarin.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Gtk;
using MonoDevelop.Components;
using MonoDevelop.Core;
using MonoDevelop.Projects;

namespace MonoDevelop.MacDev.PlistEditor
{
	[System.ComponentModel.ToolboxItem (false)]
	public partial class PListEditorWidget : Notebook
	{
		public PListEditorWidget (Project proj, PDictionary plist)
		{
			var summaryScrolledWindow = new CompactScrolledWindow ();
			var summaryLabel = new Label (GettextCatalog.GetString ("Summary"));
			AppendPage (summaryScrolledWindow, summaryLabel);
			var summaryVbox = new VBox (false, 0);
			summaryScrolledWindow.AddWithViewport (summaryVbox);
			
			var advancedScrolledWindow = new CompactScrolledWindow ();
			var advancedLabel = new Label (GettextCatalog.GetString ("Advanced"));
			AppendPage (advancedScrolledWindow, advancedLabel);
			var advancedVbox = new VBox (false, 0);
			advancedScrolledWindow.AddWithViewport (advancedVbox);
			
			IPlistEditingHandler h = new IPhonePlistEditingHandler ();
			foreach (var section in h.GetSections (proj, plist)) {
				var expander = new MacExpander () {
					ContentLabel = section.Name,
					Expandable = true,
				};
				expander.SetWidget (section.Widget);
				
				if (section.IsAdvanced) {
					advancedVbox.PackStart (expander, false, false, 0);
				} else {
					summaryVbox.PackStart (expander, false, false, 0);
				}
			}
			
			ShowAll ();
		}
		
		//	var iphone = NSDictionary.Get<PArray> ("UISupportedInterfaceOrientations");
		//	iPhoneDeploymentInfoContainer.Visible = iphone != null;
			
		//	var ipad   = NSDictionary.Get<PArray> ("UISupportedInterfaceOrientations~ipad");
		//	iPadDeploymentInfoContainer.Visible = ipad != null;
	}
}