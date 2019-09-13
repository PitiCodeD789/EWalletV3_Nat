using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint.Dialog;

namespace EV.Customer.Droid.CustomRender
{
    public class CustomDialogFragment : FingerprintDialogFragment
    {
        private TextView _helpText;
        private Button _cancelButton;
        private Button _fallbackButton;
        private Button _anotherCancelButton;
        private ImageView _icon;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CustomFingerprintDialog, container);

            var reason = view.FindViewById<TextView>(Resource.Id.fingerprint_txtReason);
            if (reason != null && Configuration?.Reason != null)
            {
                reason.Text = Configuration.Reason;
            }

            _helpText = view.FindViewById<TextView>(Resource.Id.fingerprint_txtHelp);
            _cancelButton = view.FindViewById<Button>(Resource.Id.fingerprint_btnCancel);
            _fallbackButton = view.FindViewById<Button>(Resource.Id.fingerprint_btnFallback);
            _icon = view.FindViewById<ImageView>(Resource.Id.fingerprint_imgFingerprint);

            if (DefaultColor.HasValue)
            {
                _icon?.SetColorFilter(DefaultColor.Value);
            }

            _cancelButton.Click += delegate
            {
                DetachEventHandlers();
                DismissAllowingStateLoss();
            };

            //-------------------------Custom Below this line-------------------------------//

            return view;
        }
    }
}