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
    public partial class CreateRequest : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ICustomerService serviceC;
        private readonly ICannedFoodService serviceP;
        private readonly IMainService serviceM;
        public CreateRequest(ICustomerService serviceC, ICannedFoodService serviceP,
        IMainService serviceM)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }
        private void CreateRequest_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewM> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomerFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<CannedFoodViewM> listP = serviceP.GetList();
                if (listP != null)
                {
                    comboBoxCannedFood.DisplayMember = "CannedFoodName";
                    comboBoxCannedFood.ValueMember = "Id";
                    comboBoxCannedFood.DataSource = listP;
                    comboBoxCannedFood.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void CalcAmount()
        {
            if (comboBoxCannedFood.SelectedValue != null &&
            !string.IsNullOrEmpty(textBoxTotal.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxCannedFood.SelectedValue);
                    CannedFoodViewM cannedfood = serviceP.GetElement(id);
                    int count = Convert.ToInt32(textBoxTotal.Text);
                    textBoxAmount.Text = (count * cannedfood.Cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void textBoxTotal_TextChanged(object sender, EventArgs e)
        {
            CalcAmount();
        }
        private void comboBoxCannedFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcAmount();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTotal.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCannedFood.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.CreateRequest(new RequestBindingM
                {
                    CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    CannedFoodId = Convert.ToInt32(comboBoxCannedFood.SelectedValue),
                    Total = Convert.ToInt32(textBoxTotal.Text),
                    Amount = Convert.ToInt32(textBoxAmount.Text)
                });
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
