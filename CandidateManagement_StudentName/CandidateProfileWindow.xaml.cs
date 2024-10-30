﻿using CandidateManagement.BussinessObject;
using CandidateManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CandidateManagement_StudentName
{
    /// <summary>
    /// Interaction logic for CandidateProfileWindow.xaml
    /// </summary>
    public partial class CandidateProfileWindow : Window
    {
        private int? RoleID = 0;
        private ICandidateProfileService profileService;
        private IJobPostingService jobPostingService;

        public CandidateProfileWindow()
        {
            InitializeComponent();
            profileService = new CandidateProfileService();
            jobPostingService = new JobPostingService();
        }

        public CandidateProfileWindow(int? RoleID)
        {
            InitializeComponent();
            profileService = new CandidateProfileService();
            jobPostingService = new JobPostingService();
            this.RoleID = RoleID;
            switch (RoleID)
            {
                //1.Full quyen
                //2.Dont add
                case 1:
                    break;
                case 2:
                    this.btnAdd.IsEnabled = false;
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }
        private void loadDataInit()
        {
            this.dtgCandidateProfile.ItemsSource = profileService.GetCandidateProfiles();
            this.cmbJobPosting.ItemsSource = jobPostingService.GetJobPostings();
            this.cmbJobPosting.DisplayMemberPath = "JobPostingTitle";
            this.cmbJobPosting.SelectedValuePath = "PostingId";

            txtCandidateID.Text = "";
            txtBirthday.Text = "";
            txtDescription.Text = "";
            txtImageURL.Text = "";
            txtFullName.Text = "";
            cmbJobPosting.SelectedValue = "";
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CandidateProfile candidate = new CandidateProfile();
            candidate.CandidateId = txtCandidateID.Text;
            candidate.Fullname = txtFullName.Text;
            candidate.Birthday = DateTime.Parse(txtBirthday.Text);
            candidate.ProfileUrl = txtImageURL.Text;
            //candidate.Posting = jobPostingService.GetJobPostingById(cmbJobPosting.SelectedValue.ToString());
            candidate.PostingId = cmbJobPosting.SelectedValue.ToString();
            candidate.ProfileShortDescription = txtDescription.Text;

            if (profileService.AddCandidateProfile(candidate))
            {
                MessageBox.Show("Add Successfully");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
            if (row != null)
            {
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string id = ((TextBlock)RowColumn.Content).Text;
                CandidateProfile profile = profileService.GetCandidateProfileById(id);
                if (profile != null)
                {
                    txtCandidateID.Text = txtCandidateID.Text;
                    txtBirthday.Text = txtBirthday.Text;
                    txtDescription.Text = txtDescription.Text;
                    txtImageURL.Text = txtImageURL.Text;
                    txtFullName.Text += txtFullName.Text;
                    cmbJobPosting.SelectedValue = profile.PostingId;

                }
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            CandidateProfile candidate = new CandidateProfile();
            candidate.CandidateId = txtCandidateID.Text;
            candidate.Fullname = txtFullName.Text;
            candidate.Birthday = DateTime.Parse(txtBirthday.Text);
            candidate.ProfileUrl = txtImageURL.Text;
            //candidate.Posting = jobPostingService.GetJobPostingById(cmbJobPosting.SelectedValue.ToString());
            candidate.PostingId = cmbJobPosting.SelectedValue.ToString();
            candidate.ProfileShortDescription = txtDescription.Text;

            if (profileService.UpdateCandidateProfile(candidate))
            {
                MessageBox.Show("Update Successfully");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string candidateID = txtCandidateID.Text;
            if (candidateID.Length > 0 && profileService.DeleteCandidateProfile(candidateID))
            {
                MessageBox.Show("Delete successfull");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Something's wrong!");
            }
        }
    }
}