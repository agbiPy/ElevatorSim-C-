using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Assignment_1
{
    public partial class FrmMain : Form
    {
        private DataSet ds = new DataSet();
        private OleDbConnection con;
        private OleDbDataAdapter adapter;
        private OleDbCommandBuilder builder;
        public void ChangeSize(int width, int height)
        {
            Size = new Size(width, height);
            this.Width = 10;
        }
    
        //  int sencondMove1 = DateTime.Now.Second;
        private int counterA;
        private int counterC;
        public string ibuffer;
        public string dbMsg1;
        public string dbMsg0;
        public FrmMain()
        {
            InitializeComponent();
            //Variable initialized. 
            counterA = 0;
            counterC = 0;
            dbMsg1 = null;
            dbMsg0 = null;
            ibuffer = null;
            
        }
       
        private void btnFloor_0_Click(object sender, EventArgs e)
        {
            //This method call the lift.
            tiTimer.Start();
            counterC = 0;
            btnFloor_0.Enabled = false;
            btnFloor_1.Enabled = false;
            btnCall1.Enabled = false;
            btnCall0.Enabled = false;
            btnFloor_0.BackColor = Color.Yellow;
        }

        private void tiTimer_Tick(object sender, EventArgs e)
        //Ground Floor Lift.
        {

            int sencondMove1 = DateTime.Now.Second;
            {
                picLiftCar.Top += 1;           
            }
            //lift movement from First to Ground floor
            if (picLiftCar.Top >= 400)
            {
                picLiftCar.Location = new Point(225, 390);
                tiTimer.Enabled = false;
                txtDisplay.Text = "" + picLiftCar.Top;
                lblDisplay.Text = "0";
                txtDisplay.Text = ("");
                txtDisplay.Text = ("GND Floor 0 ");
                btnFloor_1.Enabled = true;
                btnFloor_0.Enabled = true;
                btnCall0.Enabled = true;
                btnCall1.Enabled = true;
                //Ground Floor Doors Slide Open
                timDrDwnOpen.Start();
                timDrDwnClose.Start();
                btnFloor_0.BackColor = Color.LightGray;
                btnCall0.BackColor = Color.LightGray;
            // This is the lift event that display on the diaplay.
                dbMsg1 = " Lift 0";
                ibuffer = dbMsg1;
                //Call insert database Method
                InsertIntoDB("");
            }
        }
        // This Timwer is for                                                                                                                                                                                                                                                                                                                moving the Lift-car
        private void tiTime2_Tick(object sender, EventArgs e)
        {
            {
                //lift movement from Ground to first floor.
                picLiftCar.Top--;
            }
            if (picLiftCar.Top <= 39)
            {
                tiTimer2.Enabled = false;
                picLiftCar.Location = new Point(225, 33);
                lblDisplay.Text = "1";
                txtDisplay.Text = ("");
                txtDisplay.Text = ("First Floor 1 ");
                btnFloor_0.Enabled = true;
                btnCall1.Enabled = true;
                btnCall0.Enabled = true;
                // Open both doors
                timDrUpOpen.Start();
                btnFloor_1.BackColor = Color.LightGray;
                btnCall1.BackColor = Color.LightGray;
                //Autoupdate Database this will call the method to update database.
                // This is the lift event that display on the diaplay.
                dbMsg0 = " Lift 1";
                ibuffer = dbMsg0;
                //Call insert database Method
                InsertIntoDB(""); 
            }
        }
        private void btnFloor_1_Click(object sender, EventArgs e)
        {
            counterC = 0;
            btnFloor_0.Enabled = false;
            btnCall1.Enabled = false;
            btnCall0.Enabled = false;
            tiTimer2.Start();
            btnFloor_1.BackColor = Color.Yellow;
        }
       //This method will automatically update the database as the lift move up and down.
        private void InsertIntoDB(string OleDbCommand)
        {
            string connectionString = null;
            OleDbConnection con;
            OleDbDataAdapter Adapter;
            DataSet ds = new DataSet();
            //Access Database connection String.
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=C:\Users\nwoko\OneDrive\Desktop\ElevatorDB.accdb";
            string dbcommand = "Select * from EleveTB;";
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();
            con = new OleDbConnection(connectionString);
            OleDbCommand Odbcommand = new OleDbCommand(dbcommand, con);
            con = new OleDbConnection(connectionString);
            Adapter = new OleDbDataAdapter(Odbcommand);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(Adapter);

            Adapter.Fill(ds);

            DataTable dt = ds.Tables[0];
            DataRow newRow = ds.Tables[0].NewRow();

            ds.Tables[0].Rows.Add(newRow);
            //this code add the date/time and the lift events into the database
            newRow["dDate"] = date;
            newRow["tTime"] = time;
            newRow["activitiesText"] = ibuffer;
            DataSet dataSetChanges = ds.GetChanges();
            try
            {
                //update DB
                Adapter.Update(dataSetChanges);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLog_Click_1(object sender, EventArgs e)
        {
        }
        //Save Data to the Database
      
        public void reloadDataFromDbToDs()
        {
            ds.Clear();
            reloadDataFromDbToDs();
        }
        private void btnCall1_Click(object sender, EventArgs e)
        {
            counterC = 0;
            btnFloor_0.Enabled = false;
            btnCall1.Enabled = false;
            btnCall0.Enabled = false;
            tiTimer2.Start();
            btnCall1.BackColor = Color.Yellow;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Clear list box Iteam
            lisDisplay.Items.Clear();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close Applicatiom.
            Close();
        }
        private void btnCall0_Click(object sender, EventArgs e)
        {
            tiTimer.Start();
            counterC = 0;
            btnFloor_0.Enabled = false;
            btnFloor_1.Enabled = false;
            btnCall1.Enabled = false;
            btnCall0.BackColor = Color.Yellow;
        }

        private void timDrUpOpen_Tick(object sender, EventArgs e)
        //Open doors Upper doors
        {
            picLiftDrUpLeft.Left--;
            picLiftDrUpRight.Left++;
            btnFloor_0.Enabled = false;
          //  btnFloor_1.Enabled = false;
            btnCall1.Enabled = false;
            if (picLiftDrUpLeft.Left <= 20)
            {
                timDrUpOpen.Stop();
                timReference.Start();
            }
        }
        private void timDrUpClose_Tick(object sender, EventArgs e)
            //Close both upper doors, at the same time disabled all buttons while the life is traveling.
        {
            picLiftDrUpLeft.Left++;
            picLiftDrUpRight.Left--;
            btnFloor_0.Enabled = false;
            btnFloor_0.Enabled = false;
            btnCall1.Enabled = false;
         if (picLiftDrUpLeft.Left >= 221)
            {
           //Close the upper doors
            timDrUpClose.Stop();
            btnFloor_0.Enabled = true;
            btnFloor_1.Enabled = true;
            btnFloor_0.Enabled = true;
            btnCall1.Enabled = true;
            }
        }
        //Close both Ground Floor doors.
        private void timDrDwnClose_Tick(object sender, EventArgs e)
        {
              picLiftDrDownLeft.Left++;
              picLiftDrDownRight.Left--;         
            if (picLiftDrDownRight.Left >= 100) 
            {  
             timDrDwnClose.Stop(); 
            }       
        }
        //Open the doors when the lift arrive at ground floor.
        private void timDrDwnOpen_Tick(object sender, EventArgs e)
        {
            picLiftDrDownLeft.Left--;
            picLiftDrDownRight.Left++;
            if (picLiftDrDownRight.Left > 530)
            {
             timDrDwnOpen.Stop();
             timReference2.Start();
            }
        }

        private void btnLog_Click_2(object sender, EventArgs e)
        {
           // Oledb DataBase string to establish conneection between the application and the database.
            string connectionString = null;
            OleDbConnection con;
            OleDbDataAdapter Adapter;
            DataSet ds = new DataSet();
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source=C:\Users\nwoko\OneDrive\Desktop\ElevatorDB.accdb";
            string dbcommand = "Select * from EleveTB;";
            con = new OleDbConnection(connectionString);
            OleDbCommand Odbcommand = new OleDbCommand(dbcommand, con);
            con = new OleDbConnection(connectionString);
            Adapter = new OleDbDataAdapter(Odbcommand);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(Adapter);

            try
            {
                // load Database
                Adapter.Fill(ds);  
                lisDisplay.Items.Clear();

                //Display in the list box.
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lisDisplay.Items.Add(row["dDate"] + "\t" + "(" + row["tTime"] + "\t" + "(" + row["activitiesText"] + ")");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Method for closing both first floor doors. 
        private void CloseBothUpDoors()
        {
            timDrUpClose.Start();
        }
        //Method for closing both Ground floor doors. this 
        private void CloseBothGroundDoors()
        {
            timDrDwnClose.Start();
        }
        private void timReference_Tick(object sender, EventArgs e)
        {
            counterA++;

            if (counterA > -200)
            {
                CloseBothUpDoors();
            }
            if (picLiftDrDownLeft.Left > 260)
            {
                timDrUpClose.Stop();
            }
            else
            {
                timReference.Stop();
            }
        }

        private void timReference2_Tick(object sender, EventArgs e)
        {
            {
                counterC++;
                if (picLiftDrDownLeft.Left < 280)
                {
                    CloseBothGroundDoors();
                }

                if (picLiftDrDownLeft.Left > 211)
                {            
                    timDrDwnClose.Stop();
                    timReference2.Stop();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            int arg = (int)e.Argument;
        
         

            e.Result = BkgndProLogic(helperBW, arg);
            if (helperBW.CancellationPending)
            {
                e.Cancel = true;
            }
        }
        private int BkgndProLogic(BackgroundWorker bw, int a)
        {
            int result = 090;
            Thread.Sleep(20);

            MessageBox.Show("Backgroudworker in ON");
            return result;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
           // backgroundWorker1.RunWorkerAsync(2000);
        }
    }
}
  
    

    
 