using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using _EP4__MobAll_2.ContentManager;
using _EP4__MobAll_2.DataBase;
using System.ComponentModel;

namespace _EP4__MobAll_2.WindowWPF
{
    /// <summary>
    /// Логика взаимодействия для SQLFromTo.xaml
    /// </summary>
    public partial class SQLFromTo : MetroWindow
    {
        public SQLFromTo(DBInfoConnection DataBaseContext)
        {
            InitializeComponent();
            SetDBContext(DataBaseContext);
        }

        private DBInfoConnection DataBaseContext; // Коннекст к БД
        private BackgroundWorker worker;    // Асинхронка

        private void SetDBContext(DBInfoConnection a)
        {
            DataBaseContext = a;
        }

        public enum TypeSQL
        {
            UPDATE,
            ADD
        }

        private void B_Add_Click(object sender, RoutedEventArgs e)
        {
            CreateAsyn(TypeSQL.ADD);
        }

        private void B_Update_Click(object sender, RoutedEventArgs e)
        {
            CreateAsyn(TypeSQL.UPDATE);    
        }
        // Создание ассинхронного потока
        private void CreateAsyn(TypeSQL sql)
        {
            PullInformation pull = new PullInformation();
            pull.From = Convert.ToInt32(T_From.Text);
            pull.To = Convert.ToInt32(T_To.Text);
            if (pull.From > pull.To)
            {
                MessageBox.Show("От меньше До. Укажите другие значения", "Ошибка");
                return;
            }

            int fromid = FileManager.MobAllLod.IndexOf(FileManager.MobAllLod.Where(s => s.NpcID == pull.From).FirstOrDefault());
            int toid = FileManager.MobAllLod.IndexOf(FileManager.MobAllLod.Where(s => s.NpcID == pull.To).FirstOrDefault());
            pull.From = fromid;
            pull.To = toid;

            P_Progress.Value = 0;
            P_Progress.Maximum = pull.To - pull.From;
            L_progress.Content = "";

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += ActionSQL;
            worker.ProgressChanged += new ProgressChangedEventHandler(ProcessChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerComplete);
            worker.RunWorkerAsync(pull);
        }
        // Обновление прогресс бара
        private void ProcessChanged(object sender, ProgressChangedEventArgs e)
        {
            P_Progress.Value = e.ProgressPercentage + 1;
            L_progress.Content = e.ProgressPercentage;
        }
        // Завершение синхронки
        private void WorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Завершено", "SQL", MessageBoxButton.OK, MessageBoxImage.Information) ;
        }

        private void ActionSQL(object sender, DoWorkEventArgs type /*TypeSQL type*/)
        {
            // Преобразование входных данных с формы
            PullInformation info = (PullInformation)type.Argument;

            info.To++;
            for (int i = 0; info.From < info.To; info.From++, i++)
            {
                InquirySQL sql = new InquirySQL();
                var mob = new DataBase.sqlTable.Data.t_npc();
                sql.GenerateListForTable(ref mob, FileManager.MobAllLod[info.From]);
                if(info.Type == TypeSQL.ADD)
                    sql.AddThis(mob, DataBaseContext);
                else if(info.Type == TypeSQL.UPDATE)
                    sql.UpdateThis(mob, DataBaseContext);
                worker.ReportProgress(i);   // Обновить прогесс
            }
        }
    }

    class PullInformation
    {
        public int From { get; set; }
        public int To { get; set; }
        public SQLFromTo.TypeSQL Type { get; set; }
    }
}
