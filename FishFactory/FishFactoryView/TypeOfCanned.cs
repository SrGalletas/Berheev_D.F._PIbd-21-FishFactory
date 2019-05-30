using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishFactoryServiceDAL.ViewM;
using FishFactoryServiceDAL.Interfaces;
using Unity;

namespace FishFactoryView
{
    public partial class TypeOfCanned : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public TypeOfCannedViewM Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private readonly ITypeOfFishService service;
        private TypeOfCannedViewM model;
        public TypeOfCanned(ITypeOfFishService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void TypeOfCanned_Load(object sender, EventArgs e)
        {
            try
            {
                List<TypeOfFishViewM> list = service.GetList();
                if (list != null)
                {
                    comboBoxTypeOfFish.DisplayMember = "TypeOfFishName";
                    comboBoxTypeOfFish.ValueMember = "Id";
                    comboBoxTypeOfFish.DataSource = list;
                    comboBoxTypeOfFish.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxTypeOfFish.Enabled = false;
                comboBoxTypeOfFish.SelectedValue = model.TypeOfFishId;
                textBoxTotal.Text = model.Total.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTotal.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxTypeOfFish.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new TypeOfCannedViewM
                    {
                        TypeOfFishId = Convert.ToInt32(comboBoxTypeOfFish.SelectedValue),
                        TypeOfFishName = comboBoxTypeOfFish.Text,
                        Total = Convert.ToInt32(textBoxTotal.Text)
                    };
                }
                else
                {
                    model.Total = Convert.ToInt32(textBoxTotal.Text);
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
