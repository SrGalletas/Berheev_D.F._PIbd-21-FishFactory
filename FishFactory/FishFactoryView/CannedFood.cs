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
    public partial class CannedFood : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ICannedFoodService service;
        private int? id;
        private List<TypeOfCannedViewM> typeOfCanneds;
        public CannedFood(ICannedFoodService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void CannedFood_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CannedFoodViewM view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.CannedFoodName;
                        textBoxCost.Text = view.Cost.ToString();
                        typeOfCanneds = view.TypeOfCanneds;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                typeOfCanneds = new List<TypeOfCannedViewM>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (typeOfCanneds != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = typeOfCanneds;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<TypeOfCanned>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.CannedFoodId = id.Value;
                    }
                    typeOfCanneds.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<TypeOfCanned>();
                form.Model =
                typeOfCanneds[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    typeOfCanneds[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                    form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        typeOfCanneds.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCost.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (typeOfCanneds == null || typeOfCanneds.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<TypeOfCannedBindingM> typeOfCannedBM = new
                List<TypeOfCannedBindingM>();
                for (int i = 0; i < typeOfCanneds.Count; ++i)
                {
                    typeOfCannedBM.Add(new TypeOfCannedBindingM
                    {
                        Id = typeOfCanneds[i].Id,
                        CannedFoodId = typeOfCanneds[i].CannedFoodId,
                        TypeOfFishId = typeOfCanneds[i].TypeOfFishId,
                        Total = typeOfCanneds[i].Total
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new CannedFoodBindingM
                    {
                        Id = id.Value,
                        CannedFoodName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxCost.Text),
                        TypeOfCanneds = typeOfCannedBM
                    });
                }
                else
                {
                    service.AddElement(new CannedFoodBindingM
                    {
                        CannedFoodName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxCost.Text),
                        TypeOfCanneds = typeOfCannedBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

      
    }
}
