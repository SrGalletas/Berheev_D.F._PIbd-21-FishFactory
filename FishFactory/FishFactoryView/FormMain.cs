using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishFactoryServiceDAL.BindingM;
using FishFactoryServiceDAL.ViewM;
using FishFactoryServiceDAL.Interfaces;
using Unity;

namespace FishFactoryView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IMainService service;
        private readonly IReptService reptService;
        public FormMain(IMainService service, IReptService reptService)
        {
            InitializeComponent();
            this.service = service;
            this.reptService = reptService;
        }
        private void LoadData()
        {
            try
            {
                List<RequestViewM> list = service.GetList();
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<Customers>();
            form.ShowDialog();
        }
        private void компонентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<TypesOfFish>();
            form.ShowDialog();
        }
        private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CannedFoods>();
            form.ShowDialog();
        }
        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<Storages>();
            form.ShowDialog();
        }

        private void пополнитьСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FullStorage>();
            form.ShowDialog();
        }
        private void buttonCreateRequest_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CreateRequest>();
            form.ShowDialog();
            LoadData();
        }
        private void buttonTakeRequestInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.TakeRequestInWork(new RequestBindingM { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void buttonRequestReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.FinishRequest(new RequestBindingM { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void buttonPayRequest_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.PayRequest(new RequestBindingM { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void прайсИзделийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    reptService.SaveCannedFoodCost(new ReptBindingM
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
        private void загруженностьСкладовToolStripMenuItem_Click(object sender, EventArgs
        e)
        {
            var form = Container.Resolve<StoragesLoad>();
            form.ShowDialog();
        }
        private void заказыКлиентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CustomerRequests>();
            form.ShowDialog();
        }
    }
}
