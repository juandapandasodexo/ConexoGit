//using System;
//using Android.Widget;
//using Conexo.Droid.Renderers;
//using Conexo.Navigation;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android.AppCompat;

//[assembly: ExportRenderer(typeof(CustomImplementedNav), typeof(CustomMasterDetailPageRenderer))]
//namespace Conexo.Droid.Renderers
//{

//    public class CustomMasterDetailPageRenderer : MasterDetailPageRenderer
//    {

//        protected override void OnLayout(bool changed, int l, int t, int r, int b)
//        {
//            base.OnLayout(changed, l, t, r, b);

//            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

//            for (int index = 0; index < toolbar.ChildCount; index++)
//            {
//                if (toolbar.GetChildAt(index) is TextView)
//                {
//                    var title = toolbar.GetChildAt(index) as TextView;
//                    float toolbarCenter = toolbar.MeasuredWidth / 2;
//                    float titleCenter = title.MeasuredWidth / 2;
//                    title.SetX(toolbarCenter - titleCenter);
//                }
//            }
//        }

//    }

//}