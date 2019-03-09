using System;
using System.Collections.Generic;
using Android.App;
using Android.Database;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Nodes
{
    public class CustomAdapter : BaseAdapter
    {
        List<Note> items;
        Activity context;

        public CustomAdapter(Activity context, List<Note> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // taaskasutame olemasolevat vaadet(View) kui on olemas.
            if (view == null) // kui ei ole loome uue            
                view = context.LayoutInflater.Inflate(Resource.Layout.content_main, null);
            view.FindViewById<TextView>(Resource.Id.pealkiritextview).Text = items[position].Pealkiri;
            view.FindViewById<TextView>(Resource.Id.sisutextview).Text = items[position].Sisu;

            return view;
        }
    }
}