﻿#nullable enable
using Microsoft.UI.Xaml.Controls;
using WBrush = Microsoft.UI.Xaml.Media.Brush;

namespace Microsoft.Maui.Handlers
{
	public partial class DatePickerHandler : ViewHandler<IDatePicker, DatePicker>
	{
		WBrush? _defaultForeground;

		protected override DatePicker CreateNativeView() => new DatePicker();

		protected override void ConnectHandler(DatePicker nativeView)
		{
			nativeView.DateChanged += OnControlDateChanged;
		}

		protected override void DisconnectHandler(DatePicker nativeView)
		{
			nativeView.DateChanged -= OnControlDateChanged;
		}

		protected override void SetupDefaults(DatePicker nativeView)
		{
			_defaultForeground = nativeView.Foreground;

			base.SetupDefaults(nativeView);
		}

		public static void MapFormat(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateDate(datePicker);
		}

		public static void MapDate(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateDate(datePicker);
		}

		public static void MapMinimumDate(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateMinimumDate(datePicker);
		}

		public static void MapMaximumDate(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateMaximumDate(datePicker);
		}

		public static void MapCharacterSpacing(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateCharacterSpacing(datePicker);
		}

		public static void MapFont(DatePickerHandler handler, IDatePicker datePicker)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.NativeView?.UpdateFont(datePicker, fontManager);
		}

		public static void MapTextColor(DatePickerHandler handler, IDatePicker datePicker)
		{
			handler.NativeView?.UpdateTextColor(datePicker, handler._defaultForeground);
		}

		void OnControlDateChanged(object? sender, DatePickerValueChangedEventArgs e)
		{
			if (VirtualView == null)
				return;

			if (VirtualView.Date.CompareTo(e.NewDate.Date) != 0)
			{
				var date = e.NewDate.Date.Clamp(VirtualView.MinimumDate, VirtualView.MaximumDate);
				VirtualView.Date = date;

				// Set the control date-time to clamped value, if it exceeded the limits at the time of installation.
				if (date != e.NewDate.Date)
				{
					NativeView?.UpdateDate(date);
					NativeView?.UpdateLayout();
				}

				VirtualView.InvalidateMeasure();
			}
		}
	}
}