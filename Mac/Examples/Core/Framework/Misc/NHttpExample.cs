using System;

using Nevron.Nov.DataStructures;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;
using Nevron.Nov.Networking;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Framework
{
	public class NHttpExample : NExampleBase
	{
		#region Constructors

		public NHttpExample()
		{
		}
		static NHttpExample()
		{
			NHttpExampleSchema = NSchema.Create(typeof(NHttpExample), NExampleBase.NExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override NWidget CreateExampleContent()
		{
			NStackPanel stack = new NStackPanel();
			stack.FitMode = ENStackFitMode.Last;
			stack.FillMode = ENStackFillMode.Last;

			stack.Add(CreatePredefinedRequestsWidget());
			stack.Add(CreateCustomRequestWidget());
			
			m_ResponseContentHolder = new NContentHolder();
			stack.Add(m_ResponseContentHolder);
			return stack;
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();
			stack.FillMode = ENStackFillMode.Last;
			stack.FitMode = ENStackFitMode.Last;

			// clear button
			NButton button = new NButton("Clear Requests");
			button.Content.HorizontalPlacement = ENHorizontalPlacement.Center;
			button.Click += new Function<NEventArgs>(OnClearRequestsListBoxButtonClick);
			stack.Add(button);

			// create the requests list box in which we add the submitted requests.
			m_RequestsListBox = new NListBox();
			stack.Add(m_RequestsListBox);

			return new NGroupBox("Requests", stack);
		}
        protected override string GetExampleDescription()
        {
            return @"
<p>
Demonstrates the HTTP protocol wrapper that comes along with. It allows you to make HTTP requests from a single code base.
</p>
";
        }

		#endregion

		#region Implementation - User Interface

		private NWidget CreatePredefinedRequestsWidget()
		{
			NGroupBox groupBox = new NGroupBox("Predefined Requests");

			NStackPanel stack = new NStackPanel();
			groupBox.Content = stack;
			stack.Direction = ENHVDirection.LeftToRight;

			// get Google logo
			NButton getGoogleLogoButton = new NButton("Get Google LOGO");
			getGoogleLogoButton.Click += new Function<NEventArgs>(GetGoogleLogoClick);
			stack.Add(getGoogleLogoButton);

			// get Google thml
			NButton getGoogleHtmlButton = new NButton("Get Google HTML");
			getGoogleHtmlButton.Click += new Function<NEventArgs>(GetGoogleHtmlClick);
			stack.Add(getGoogleHtmlButton);

			// get wikipedia logo
			NButton getWikipediaLogoButton = new NButton("Get Wikipedia LOGO");
			getWikipediaLogoButton.Click += new Function<NEventArgs>(OnGetWikipediaLogoClick);
			stack.Add(getWikipediaLogoButton);

			// get wikipedia home page HTML
			NButton getWikipediaHtmlButton = new NButton("Get Wikipedia HTML");
			getWikipediaHtmlButton.Click += new Function<NEventArgs>(OnGetWikipediaHtmlClick);
			stack.Add(getWikipediaHtmlButton);

			return groupBox;
		}
		private NWidget CreateCustomRequestWidget()
		{
			NGroupBox groupBox = new NGroupBox("Custom Request");

			NDockPanel dock = new NDockPanel();
			groupBox.Content = dock;

			NLabel label = new NLabel("URI:");
			label.VerticalPlacement = ENVerticalPlacement.Center;
			NDockLayout.SetDockArea(label, ENDockArea.Left);
			dock.Add(label);

			m_CustomURITextBox = new NTextBox();
			m_CustomURITextBox.Text = "http://www.nevron.com/gallery/FullGalleries/chart/pie/images/3D-pie-cut-edge-ring.png";
			m_CustomURITextBox.Padding = new NMargins(0, 3, 0, 3);
			NDockLayout.SetDockArea(m_CustomURITextBox, ENDockArea.Center);
			dock.Add(m_CustomURITextBox);

			NButton submitButton = new NButton("Submit");
			NDockLayout.SetDockArea(submitButton, ENDockArea.Right);
			submitButton.Click += new Function<NEventArgs>(OnSumbitCustomRequestClick);
			dock.Add(submitButton);

			return groupBox;
		}

		#endregion

		#region Implementation - Event Handlers - Predefined Requests
		
		private void GetGoogleLogoClick(NEventArgs args)
		{
			// create a HTTP request for the Google logo and subscribe for Completed event
			string googleLogoURI = "http://www.google.com/images/srpr/logo3w.png";
			NHttpRequest request = new NHttpRequest(googleLogoURI);
			request.Headers[NHttpHeaderFieldName.Accept] = "image/png";
			request.Completed += new Function<NUriRequest, NUriResponse>(OnRequestCompleted);

			// create a list box item for the request, prior to submittion and submit the request
			CreateRequestListBoxItem(request);
			request.Submit();
		}
		private void GetGoogleHtmlClick(NEventArgs args)
		{
			// create a HTTP request for the Google home page
			string googleHtmlURI = "http://www.google.com";
			NHttpRequest request = new NHttpRequest(googleHtmlURI);
			request.Completed += OnRequestCompleted;

			// create a list box item for the request, prior to submition and submit the request
			CreateRequestListBoxItem(request);
			request.Submit();
		}
		private void OnGetWikipediaLogoClick(NEventArgs args)
		{
			// create a HTTP request for the Wikipedia logo and subscribe for Completed event
			string wikipediaLogoURI = "http://upload.wikimedia.org/wikipedia/commons/6/63/Wikipedia-logo.png";
			NHttpRequest request = new NHttpRequest(wikipediaLogoURI);
			request.Completed += OnRequestCompleted;

			// create a list box item for the request, prior to submittion and submit the request
			CreateRequestListBoxItem(request);
			request.Submit();
		}
		private void OnGetWikipediaHtmlClick(NEventArgs args)
		{
			// create a HTTP request for the Wikipedia home page and subscribe for Completed event
			string wikipediaHtmlURI = "http://www.wikimedia.org";
			NHttpRequest request = new NHttpRequest(wikipediaHtmlURI);
			request.Completed += OnRequestCompleted;

			// create a list box item for the request, prior to submittion and submit the request
			CreateRequestListBoxItem(request);
			request.Submit();
		}

		#endregion

		#region Implementation - Event Handlers - Custom Requests

		private void OnSumbitCustomRequestClick(NEventArgs args)
		{
			try
			{
				// create a HTTP request for the custom URI and subscribe for Completed event
				string uri = m_CustomURITextBox.Text;
				NHttpRequest request = new NHttpRequest(uri);
				request.AllowCache = false;
				request.AllowServeFromCache = false;
				request.Completed += new Function<NUriRequest, NUriResponse>(OnRequestCompleted);

				// create a list box item for the request, prior to submittion and submit the request
				CreateRequestListBoxItem(request);
				request.Submit();
			}
			catch (Exception ex)
			{
				NMessageBox.Show("Failed to submit custom request.\n\nException was: " + ex.Message, "Failed to submit custom request",
					ENMessageBoxButtons.OK, ENMessageBoxIcon.Error);
			}
		}

		#endregion

		#region Implementation - Event Handlers - Request/Response

		/// <summary>
		/// Called by a NHttpRequest when it has been completed.
		/// </summary>
		/// <param name="response"></param>
		private void OnRequestCompleted(NUriRequest request, NUriResponse response)
		{
			NHttpRequest httpRequest = (NHttpRequest)request;
			NHttpResponse httpResponse = (NHttpResponse)response;

			// update the list box item
			UpdateRequestListBoxItem(httpRequest, httpResponse);

			// update the response content holder
			switch (response.Status)
			{
				case ENAsyncResponseStatus.Aborted:
					// request has been aborted by the user -> do nothing.
					break;

				case ENAsyncResponseStatus.Failed:
					// request has failed -> fill content with an error message
					m_ResponseContentHolder.Content = new NLabel("Request for URI: " + request.Uri.ToString() + " failed. Error was: " + response.ErrorException.ToString());
					break;

				case ENAsyncResponseStatus.Succeeded:
					// request succeded -> fill content with the response content
					try
					{
						// get the Content-Type Http Header field, and split it to portions
						// NOTE: the Content-Type is a multi value field. Values are seperated with the ';' char
						string contentType = httpResponse.HeaderFields[NHttpHeaderFieldName.ContentType];
						string[] contentTypes = contentType.Split(new char[]{';'});

						// normalize content type values (trim and make lower case)
						for (int i = 0; i < contentTypes.Length;i++)
						{
							contentTypes[i] = contentTypes[i].Trim();
							contentTypes[i] = contentTypes[i].ToLower();
						}

						// the first part of the content type is the mime type of the content
						switch (contentTypes[0])
						{
							case "image/png":
							case "image/jpeg":
							case "image/bmp":
								NImage image = new NImage(new NBytesImageSource(response.Data));
								NImageBox imageBox = new NImageBox(image);
								m_ResponseContentHolder.Content = new NScrollContent(imageBox);
								break;

							case "text/html":
							case "application/json":
								string charSet = (contentTypes.Length >= 1? contentTypes[1]: "charset=utf-8");
								string html = "";
								switch (charSet)
								{
									case "charset=utf-8":
										html = Nevron.Nov.Text.NEncoding.UTF8.GetString(response.Data);
										break;

									default:
										html = Nevron.Nov.Text.NEncoding.UTF8.GetString(response.Data);
										break;
								}

								NTextBox textBox = new NTextBox();
								textBox.Text = html;
								m_ResponseContentHolder.Content = textBox;
								break;

							default:
								break;
						}
					}
					catch (Exception ex)
					{
						m_ResponseContentHolder.Content = new NLabel("Request for URI: " + request.Uri.ToString() + " decoding failed. Error was: " + ex.Message.ToString());
					}
					break;
			}
		}
		private void OnClearRequestsListBoxButtonClick(NEventArgs args)
		{
			m_RequestsListBox.Items.Clear();
			m_Request2ListBoxItem.Clear();
		}
		private void OnAbortRequestButtonClick(NEventArgs args)
		{
			// get the request form the button tag (see CreateRequestListBoxItem) and abort it
			NHttpRequest request = (NHttpRequest)args.TargetNode.Tag;
			request.Abort();
		}
		private void OnViewResponseHeadersButtonClick(NEventArgs args)
		{
			// get the response form the button tag (see UpdateRequestListBoxItem) and display its headers
			object[] array = (object[])args.TargetNode.Tag;

			NHttpRequest request = (NHttpRequest)array[0];
			NHttpResponse response = (NHttpResponse)array[1];

			// create a top level window, setup as a dialog
			NTopLevelWindow window = NApplication.CreateTopLevelWindow();
			window.SetupDialogWindow(request.Uri.ToString(), true);

			// create a list box for the headers
			NListBox listBox = new NListBox();
			window.Content = listBox;

			// fill with header fields
			INIterator<NHttpHeaderField> it = response.HeaderFields.GetIterator();
			while(it.MoveNext())
			{
				listBox.Items.Add(new NListBoxItem(it.Current.ToString()));
			}

			// open the window
			window.Open();
		}

		#endregion

		#region Implementation - Requests List

		/// <summary>
		/// Called when a request is about to be submitted. Adds a new entry in the requests list box.
		/// </summary>
		/// <param name="request"></param>
		private void CreateRequestListBoxItem(NHttpRequest request)
		{
			NGroupBox groupBox = new NGroupBox(new NLabel("URI: " + request.Uri.ToString()));
			groupBox.HorizontalPlacement = ENHorizontalPlacement.Fit;

			NStackPanel stack = new NStackPanel();
			stack.HorizontalPlacement = ENHorizontalPlacement.Fit;
			groupBox.Content = stack;            

			NStackPanel hstack = new NStackPanel();
			hstack.Direction = ENHVDirection.LeftToRight;
			hstack.HorizontalPlacement = ENHorizontalPlacement.Fit;
			hstack.FillMode = ENStackFillMode.None;
			hstack.FitMode = ENStackFitMode.Equal;
			stack.Add(hstack);
			
			// create the abort button. 
			// NOTE: the request is recorded in the button Tag
			NButton abortButton = new NButton("Abort");
			abortButton.Click += new Function<NEventArgs>(OnAbortRequestButtonClick);
			abortButton.Tag = request;
			hstack.Add(abortButton);

			NButton headersButton = new NButton("View Response Headers");
			headersButton.Click += new Function<NEventArgs>(OnViewResponseHeadersButtonClick);
			headersButton.Tag = request;
			headersButton.Enabled = false;
			hstack.Add(headersButton);

			// add item
			NListBoxItem item = new NListBoxItem(groupBox);
			item.BorderThickness = new NMargins(2);
			item.Border = null;
			m_RequestsListBox.Items.Add(item);
			m_Request2ListBoxItem.Add(request, item);
		}
		/// <summary>
		/// Called when a request has been completed. Updates the item for the request.
		/// </summary>
		/// <param name="request"></param>
		private void UpdateRequestListBoxItem(NHttpRequest request, NHttpResponse response)
		{
			// first clear the boder of all items
			for (int i = 0; i < m_RequestsListBox.Items.Count; i++)
			{
				m_RequestsListBox.Items[i].Border = null;
			}

			// highlight the completed item in red
			NListBoxItem item = m_Request2ListBoxItem[request];
			item.Border = NBorder.CreateFilledBorder(NColor.LightCoral);

			// update the group box header
			NGroupBox groupBox = (NGroupBox)item.Content;
			NLabel headerLabel = (NLabel)groupBox.Header.Content;
			headerLabel.Text += " Response Status: " + response.Status.ToString() + ", Received In: " + (response.ReceivedAt - request.SentAt).TotalSeconds.ToString() + " seconds";

			// Disable the Abort button (the first button of the item (first descentant of type button))
			NButton abortButton = (NButton)item.GetFirstDescendant(NIsFilter<NNode, NButton>.Instance);
			abortButton.Enabled = false;

			// Enable the Headers Button (the last button of the item)
			NButton headersButton = (NButton)item.GetLastDescendant(NIsFilter<NNode, NButton>.Instance);
			headersButton.Tag = new object[]{ request, response };
			headersButton.Enabled = true;
		}

		#endregion

		#region Fields

		/// <summary>
		/// A content holder for the content of the last completed request.
		/// </summary>
		private NContentHolder m_ResponseContentHolder;
		/// <summary>
		/// A text box in which the user enters the URI for a custom request.
		/// </summary>
		private NTextBox m_CustomURITextBox;
		/// <summary>
		/// The list in which we add information about the sumbitted requests.
		/// </summary>
		private NListBox m_RequestsListBox;
		/// <summary>
		/// A map for the requests 2 list box items.
		/// </summary>
		private NMap<NHttpRequest, NListBoxItem> m_Request2ListBoxItem = new NMap<NHttpRequest, NListBoxItem>();

		#endregion

		#region Schema

		public static readonly NSchema NHttpExampleSchema;

		#endregion
	}
}