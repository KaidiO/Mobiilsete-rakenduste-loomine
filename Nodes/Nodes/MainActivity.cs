using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SQLite;
using static Android.Widget.AdapterView;

namespace Nodes
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<Note> ListOfNodes;
        SQLiteConnection db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;                      
            
            // Create your application here
            CreateDatabase();
            var listView = FindViewById<ListView>(Resource.Id.listView1);
            var pealkiriEditText = FindViewById<EditText>(Resource.Id.editText1);
            var sisuEditText = FindViewById<EditText>(Resource.Id.editText2);
            var addButton = FindViewById<Button>(Resource.Id.button1);

            var listOfNodes = GetAllNotesFromDatabase();

            ListOfNodes = listOfNodes.ToList();
            listView.Adapter = new CustomAdapter(this, ListOfNodes);

            listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                
                Toast.MakeText(this, "Vajutasid", ToastLength.Short).Show();
            };

            addButton.Click += delegate
            {
                var list = new Note();
                list.Pealkiri = pealkiriEditText.Text;
                list.Sisu = sisuEditText.Text;
                ListOfNodes.Add(list);
                listView.Adapter = new CustomAdapter(this, ListOfNodes);
                SaveNotesToDatabase(list);

            };


            var toSecondActivityButton = FindViewById<Button>(Resource.Id.button2);
            toSecondActivityButton.Click += delegate
            {
                var secondActivity = new Intent(this, typeof(SecondActivity));
                secondActivity.PutExtra("MyData", "Hello World!!!!!!!!!!!!");
                StartActivity(secondActivity);
            };

        }
        public void SaveNotesToDatabase(Note note)
        {
            
             db.Insert(note);
            
        }

        public TableQuery<Note> GetAllNotesFromDatabase()
        {
            //Andmebaasist välja küsimine
            return db.Table<Note>();
        }

        public void CreateDatabase()
        {
            //Loome andmebaasi
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
            db = new SQLiteConnection(dbPath);
            //Loome tabeli andmebaasi
            db.CreateTable<Note>();
        }
               

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

    }   
}









