using System;
using AppQuo.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewSeparated))]
namespace AppQuo.iOS.Renderers
{

    public class ListViewSeparated : ListViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            // ListView Separator for whole view width
            Control.SeparatorInset = UIEdgeInsets.Zero;
            Control.LayoutMargins = UIEdgeInsets.Zero;
            Control.CellLayoutMarginsFollowReadableWidth = false;

            // ListView Separator - remove it, from empty cells
            Control.TableFooterView = new UIView();
        }

    }

}