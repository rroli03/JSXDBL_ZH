using JSXDBL.Models;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace JSXDBL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CarsContext context = new CarsContext();

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

        private void Form1_Load(object sender, EventArgs e)
        {
            FilterMake();
        }

        private void FilterMake()
        {
            var makes = from x in context.Make
                        where x.MakeName.Contains(textBoxSearch.Text)
                        select x;
            listBoxMakes.DataSource = makes.ToList();
            listBoxMakes.DisplayMember = "MakeName";
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterMake();
        }

        private void listBoxMakes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListCars();
        }

        private void ListCars()
        {
            var selectedmake = (Make)listBoxMakes.SelectedValue;
            var cars = from x in context.Cars
                       where x.MakeFk == selectedmake.MakeSk
                       select new DetailedCars
                       {
                           Model = x.Model,
                           Fuel = x.FuelFkNavigation.FuelName,
                           Gear = x.GearFkNavigation.GearName
                       };
            detailedCarsBindingSource.DataSource = cars.ToList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            NewForm newformadd = new NewForm();
            if (newformadd.ShowDialog() == DialogResult.OK)
            {
                Cars newcar = new Cars();
                newcar.Model = newformadd.textBoxName.Text;
                newcar.MakeFk = newformadd.comboBoxMake.SelectedIndex + 1;
                newcar.FuelFk = newformadd.comboBoxFuel.SelectedIndex + 1;
                newcar.GearFk = newformadd.comboBoxGear.SelectedIndex + 1;
                context.Add(newcar);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);                
                }
                ListCars();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to leave this form?", "Form Leaving Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to remove this car?", "Car Removing Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var currentmodel = ((DetailedCars)detailedCarsBindingSource.Current).Model;
                var remove = (from x in context.Cars
                              where x.Model == currentmodel
                              select x).FirstOrDefault();
                context.Cars.Remove(remove);
                context.SaveChanges();
                ListCars();
            }

        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            CreateExcel();
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;
                Createtable();
                xlApp.Visible = true;
                xlApp.UserControl = true;

            }
            catch (Exception ex)
            {
                string ErrorMessage = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(ErrorMessage, "Error");
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void Createtable()
        {
            string[] fejlecek = new string[]
                        {
                "Make",
                "Model",
                "Fuel",
                "Gear"
                        };
            for (int i = 0; i < fejlecek.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = fejlecek[i];
            }
            Models.CarsContext carscontext = new Models.CarsContext();
            var models = carscontext.Cars.ToList();

            object[,] adattomb = new object[models.Count(), fejlecek.Count()];

            for (int i = 0; i < models.Count(); i++)
            {
                adattomb[i, 0] = models[i].MakeFk;
                adattomb[i, 1] = models[i].Model;
                adattomb[i, 2] = models[i].FuelFk;
                adattomb[i, 3] = models[i].GearFk;
            }

            int sorokszama = adattomb.GetLength(0);
            int oszlopokszama = adattomb.GetLength(1);

            Excel.Range adatrange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokszama, oszlopokszama);
            adatrange.Value2 = adattomb;
            adatrange.Columns.AutoFit();
            Excel.Range fejlecrange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejlecrange.EntireColumn.AutoFit();
            fejlecrange.RowHeight = 40;
        }
    }

    public class DetailedCars
    {
        public string? Model { get; set; }

        public string? Fuel { get; set; } 

        public string? Gear { get; set; }
    }
}