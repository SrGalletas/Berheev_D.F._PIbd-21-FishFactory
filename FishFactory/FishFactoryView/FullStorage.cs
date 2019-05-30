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
    public partial class FullStorage : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IStorageService serviceS;
        private readonly ITypeOfFishService serviceC;
        private readonly IMainService serviceM;
        public FullStorage(IStorageService serviceS, ITypeOfFishService serviceC,
        IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }
        private void FullStorage_Load(object sender, EventArgs e)
        {
            try
            {
                List<TypeOfFishViewM> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxTypeOfFish.DisplayMember = "TypeOfFishName";
                    comboBoxTypeOfFish.ValueMember = "Id";
                    comboBoxTypeOfFish.DataSource = listC;
                    comboBoxTypeOfFish.SelectedItem = null;
                }
                List<StorageViewM> listS = serviceS.GetList();
                if (listS != null)
                {
                    
                comboBoxStorage.DisplayMember = "StorageName";
                    comboBoxStorage.ValueMember = "Id";
                    comboBoxStorage.DataSource = listS;
                    comboBoxStorage.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
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
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.PutTypeOfFishOnStorage(new StorageFishBindingM
                {
                    TypeOfFishId = Convert.ToInt32(comboBoxTypeOfFish.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorage.SelectedValue),
                    Total = Convert.ToInt32(textBoxTotal.Text)
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
