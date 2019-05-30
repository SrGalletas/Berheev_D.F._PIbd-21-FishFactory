using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace FishFactoryView
{
    public partial class StoragesLoad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IReptService service;
        public StoragesLoad(IReptService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void StoragesLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = service.GetStoragesLoad();
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.StorageNominal, "", "" });
                        foreach (var listElem in elem.TypesOfFish)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalTotal });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    service.SaveStoragesLoad(new ReptBindingM
                    {
                        FileNominal = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
    }
}
