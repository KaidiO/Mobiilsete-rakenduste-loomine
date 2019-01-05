using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Widget.AdapterView;
using SQLite;
using System.IO;

namespace FirstApp
{
    [Activity(Label = "ListOfThingsActivity")]
    public class ListOfThingsActivity : Activity
    {
        List<Car> ListOfCars;
        SQLiteConnection db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.list_layout);
            // Create your application here
            CreateDatabase();          
            var listView = FindViewById<ListView>(Resource.Id.listView1);
            var nameEditText = FindViewById<EditText>(Resource.Id.editText1);
            var modelEditText = FindViewById<EditText>(Resource.Id.editText2);
            var kwEditText = FindViewById<EditText>(Resource.Id.editText3);
            var addButton = FindViewById<Button>(Resource.Id.button1);
            
            var listOfCars = GetAllCarsFromDatabase();

            ListOfCars = listOfCars.ToList();
            listView.Adapter = new CustomAdapter(this, ListOfCars);         

            listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {                
                Toast.MakeText(this, "Vajutasid", ToastLength.Short).Show();                
            };

            addButton.Click += delegate
            {
                var car = new Car();
                car.Name = nameEditText.Text;
                car.Model = modelEditText.Text;
                car.Kw = int.Parse(kwEditText.Text);
                ListOfCars.Add(car);
                listView.Adapter = new CustomAdapter(this, ListOfCars);
                SaveCarsToDatabase();

            };

            
        }

        public void SaveCarsToDatabase()
        {
            foreach (var car in ListOfCars)
            {
                //andmebaasi lisamine
                db.Insert(car);
            }
        }

        public TableQuery<Car> GetAllCarsFromDatabase()
        {
            //Andmebaasist välja küsimine
           return db.Table<Car>();            
        }

        public void CreateDatabase()
        {
            //Loome andmebaasi
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
            db = new SQLiteConnection(dbPath);
            //Loome tabeli andmebaasi
            db.CreateTable<Car>();
        }

        private void GenerateCars()
        {
            var listOfCars = new List<Car>();

            var car = new Car();
            car.Name = "Ferrari";
            car.Model = "Modena";
            car.Kw = 360;
            listOfCars.Add(car);

            car = new Car();
            car.Name = "Volkswagen";
            car.Model = "Passat";
            car.Kw = 81;
            listOfCars.Add(car);

            car = new Car();
            car.Name = "Tesla";
            car.Model = "Model S";
            car.Kw = 380;
            listOfCars.Add(car);

            car = new Car();
            car.Name = "Lamborghini";
            car.Model = "Gallardo";
            car.Kw = 500;
            listOfCars.Add(car);

            car = new Car();
            car.Name = "Lada";
            car.Model = "108";
            car.Kw = 30;
            listOfCars.Add(car);

            ListOfCars = listOfCars;
        }


    }
}