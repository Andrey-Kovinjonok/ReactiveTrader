using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Adaptive.ReactiveTrader.Client.Android.UI.SpotTiles
{
    public class SpotTileViewHolder : RecyclerView.ViewHolder
    {
        public TextView CurrencyPairLabel { get; private set; }

        public SpotTileViewHolder(View itemView) 
            : base(itemView)
        {
            CurrencyPairLabel = itemView.FindViewById<TextView>(Resource.Id.SpotTileCurrencyPairTextView);
        }
    }
}