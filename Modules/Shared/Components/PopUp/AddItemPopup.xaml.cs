﻿using CommunityToolkit.Maui.Views;

namespace Drrobo.Modules.Shared.Components.PopUp;

public partial class AddItemPopup : Popup
{
	public AddItemPopup()
	{
		InitializeComponent();
		NameEntry.Focus();
	}
}