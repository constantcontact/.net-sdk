using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CTCT;
using CTCT.Components.Contacts;
using CTCT.Components.EmailCampaigns;
using System.Globalization;
using CTCT.Exceptions;
using System.Configuration;

namespace CTCTWrapper_EmailCampaign
{
    public partial class frmEmailCampaign : Form
    {
        #region Properties

        ConstantContact _constantContact = null;
        private string _apiKey = string.Empty;
        private string _accessToken = string.Empty;

        #endregion Properties

        #region Form/Controls Events

        public frmEmailCampaign()
        {
            InitializeComponent();
            _apiKey = ConfigurationManager.AppSettings["APIKey"];
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmailCampaign_Load(object sender, EventArgs e)
        {
            try
            {
                string state = "ok";
                _accessToken = OAuth.AuthenticateFromWinProgram(ref state);

                if (string.IsNullOrEmpty(_accessToken))
                {
                    Application.Exit();
                }

                //initialize ConstantContact member
                _constantContact = new ConstantContact(_apiKey, _accessToken);
            }
            catch (OAuth2Exception oauthEx)
            {
                MessageBox.Show(string.Format("Authentication failure: {0}", oauthEx.Message), "Warning");
            }

            PopulateCampaignTypeList();
            PopulateListOfCountries();
            PopulateUSAndCanadaListOfStates();

            GetListOfContacts();
            PopulateEmailLists();
        }

        private void chkIncludeForwardEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncludeForwardEmail.Checked)
            {
                txtIncludeForwardEmail.Enabled = true;
            }
            else
            {
                txtIncludeForwardEmail.Enabled = false;
            }
        }

        private void chkIncludeSubscribeLink_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncludeSubscribeLink.Checked)
            {
                txtIncludeSubscribeLink.Enabled = true;
            }
            else
            {
                txtIncludeSubscribeLink.Enabled = false;
            }
        }

        private void rbnScheduled_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnScheduled.Checked)
            {
                txtScheduleDate.Enabled = true;
            }
            else
            {
                txtScheduleDate.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;

            try
            {
                EmailCampaign campaign = CreateCampaignFromInputs();

                EmailCampaign savedCampaign = _constantContact.AddCampaign(campaign);

                if (savedCampaign != null)
                {
                    //campaign was saved, but need to schedule it, if the case
                    Schedule schedule = null;

                    if (rbnSendNow.Checked || rbnScheduled.Checked)
                    {
                        if (rbnSendNow.Checked)
                        {
                            schedule = new Schedule() { ScheduledDate = DateTime.Now.AddMinutes(20).ToUniversalTime() };
                        }
                        else
                        {
                            schedule = new Schedule() { ScheduledDate = Convert.ToDateTime(txtScheduleDate.Text.Trim()).ToUniversalTime() };
                        }

                        Schedule savedSchedule = _constantContact.AddSchedule(savedCampaign.Id, schedule);

                        if (savedSchedule != null)
                        {
                            MessageBox.Show("Campaign successfully saved and scheduled!", "Success");
                        }
                        else
                        {
                            MessageBox.Show("Campaign was saved, but failed to schedule it!", "Failure");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Campaign successfully saved!", "Success");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to save campaign!", "Failure");
                }
            }
            catch (IllegalArgumentException illegalEx)
            {
                MessageBox.Show(GetExceptionsDetails(illegalEx, "IllegalArgumentException"), "Exception");
            }
            catch (CtctException ctcEx)
            {
                MessageBox.Show(GetExceptionsDetails(ctcEx, "CtctException"), "Exception");
            }
            catch (OAuth2Exception oauthEx)
            {
                MessageBox.Show(GetExceptionsDetails(oauthEx, "OAuth2Exception"), "Exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetExceptionsDetails(ex, "Exception"), "Exception");
            }

            btnSave.Enabled = true;
        }

        #endregion Form/Controls Events

        #region Private methods

        /// <summary>
        /// Populate the list of campaign templates
        /// </summary>
        private void PopulateCampaignTypeList()
        {
            cbCampaignType.Items.Insert(0, new ItemInfo(TemplateType.CUSTOM.ToString(), TemplateType.CUSTOM.ToString()));
            cbCampaignType.Items.Add(new ItemInfo(TemplateType.STOCK.ToString(), TemplateType.STOCK.ToString()));

            cbCampaignType.ValueMember = "Value";
            cbCampaignType.DisplayMember = "Name";

            cbCampaignType.SelectedIndex = 0;
        }

        /// <summary>
        /// Populates country list
        /// </summary>
        private void PopulateListOfCountries()
        {
            List<RegionInfo> countries = new List<RegionInfo>();
            CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo cul in cinfo)
            {
                var country = new RegionInfo(cul.LCID);

                if (country != null && !countries.Contains(country))
                {
                    countries.Add(country);
                }
            }

            countries = countries.OrderBy(c => c.EnglishName).ToList();
            cbCountry.Items.Add(new ItemInfo(string.Empty, string.Empty));

            var US = countries.Where(c => c.EnglishName.Equals("United States")).FirstOrDefault();
            var Canada = countries.Where(c => c.EnglishName.Equals("Canada")).FirstOrDefault();

            if (US != null)
            {
                countries.Remove(US);
            }

            if (Canada != null)
            {
                countries.Remove(Canada);
            }

            var lstCountries = countries.Select(c => new ItemInfo(c.EnglishName, c.TwoLetterISORegionName)).ToArray();

            if (US != null)
            {
                cbCountry.Items.Add(new ItemInfo(US.EnglishName, US.TwoLetterISORegionName));
            }

            if (Canada != null)
            {
                cbCountry.Items.Add(new ItemInfo(Canada.EnglishName, Canada.TwoLetterISORegionName));
            }

            cbCountry.Items.AddRange(lstCountries);
            cbCountry.ValueMember = "TwoLetterCountryName";
            cbCountry.DisplayMember = "Name";
        }

        /// <summary>
        /// Populates contact lists
        /// </summary>
        private void GetListOfContacts()
        {
            try
            {
                var lists = _constantContact.GetLists(null);

                if (lists != null)
                {
                    foreach (var item in lists)
                    {
                        lstContactList.Items.Add(new ItemInfo(item.Name, item.Id.ToString()));
                    }

                    lstContactList.ValueMember = "Value";
                    lstContactList.DisplayMember = "Name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to load contact lists. Error message: {0}", ex.Message), "Warning");
            }
        }

        /// <summary>
        /// Populates list of emails with a list of verified email addresses
        /// </summary>
        private void PopulateEmailLists()
        {
            List<ItemInfo> lstItems = new List<ItemInfo>();
            try {
                var emails = _constantContact.GetVerifiedEmailAddress();

                if (emails != null)
                {
                    foreach (var item in emails)
                    {
                        lstItems.Add(new ItemInfo(item.EmailAddr, item.EmailAddr));
                    }
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to retrieve list of verified email addresses:\n {0}", ex.Message), "Warning");
            }

            cbFromEmail.Items.AddRange(lstItems.ToArray());
            cbFromEmail.ValueMember = "Value";
            cbFromEmail.DisplayMember = "Name";
            cbFromEmail.SelectedIndex = 0;

            cbReplyToEmail.Items.AddRange(lstItems.ToArray());
            cbReplyToEmail.ValueMember = "Value";
            cbReplyToEmail.DisplayMember = "Name";
            cbReplyToEmail.SelectedIndex = 0;
        }

        /// <summary>
        /// Populates list of US/Canada states/provinces
        /// </summary>
        private void PopulateUSAndCanadaListOfStates()
        {
            List<ItemInfo> lstItems = new List<ItemInfo>();

            //US states
            lstItems.Add(new ItemInfo(string.Empty, string.Empty));
            lstItems.Add(new ItemInfo("AL", "Alabama"));
            lstItems.Add(new ItemInfo("AK", "Alaska"));
            lstItems.Add(new ItemInfo("AS", "American Samoa"));
            lstItems.Add(new ItemInfo("AZ", "Arizona"));
            lstItems.Add(new ItemInfo("AR", "Arkansas"));
            lstItems.Add(new ItemInfo("CA", "California"));
            lstItems.Add(new ItemInfo("CO", "Colorado"));
            lstItems.Add(new ItemInfo("CT", "Connecticut"));
            lstItems.Add(new ItemInfo("DE", "Delaware"));
            lstItems.Add(new ItemInfo("DC", "District Of Columbia"));
            lstItems.Add(new ItemInfo("FL", "Florida"));
            lstItems.Add(new ItemInfo("GA", "Georgia"));
            lstItems.Add(new ItemInfo("GU", "Guam"));
            lstItems.Add(new ItemInfo("HI", "Hawaii"));
            lstItems.Add(new ItemInfo("ID", "Idaho"));
            lstItems.Add(new ItemInfo("IL", "Illinois"));
            lstItems.Add(new ItemInfo("IN", "Indiana"));
            lstItems.Add(new ItemInfo("IA", "Iowa"));
            lstItems.Add(new ItemInfo("KS", "Kansas"));
            lstItems.Add(new ItemInfo("KY", "Kentucky"));
            lstItems.Add(new ItemInfo("LA", "Louisiana"));
            lstItems.Add(new ItemInfo("ME", "Maine"));
            lstItems.Add(new ItemInfo("MH", "Marshall Islands"));
            lstItems.Add(new ItemInfo("MD", "Maryland"));
            lstItems.Add(new ItemInfo("MA", "Massachusetts"));
            lstItems.Add(new ItemInfo("MI", "Michigan"));
            lstItems.Add(new ItemInfo("MN", "Minnesota"));
            lstItems.Add(new ItemInfo("MS", "Mississippi"));
            lstItems.Add(new ItemInfo("MO", "Missouri"));
            lstItems.Add(new ItemInfo("MT", "Montana"));
            lstItems.Add(new ItemInfo("NE", "Nebraska"));
            lstItems.Add(new ItemInfo("NV", "Nevada"));
            lstItems.Add(new ItemInfo("NH", "New Hampshire"));
            lstItems.Add(new ItemInfo("NJ", "New Jersey"));
            lstItems.Add(new ItemInfo("NM", "New Mexico"));
            lstItems.Add(new ItemInfo("NY", "New York"));
            lstItems.Add(new ItemInfo("NC", "North Carolina"));
            lstItems.Add(new ItemInfo("ND", "North Dakota"));
            lstItems.Add(new ItemInfo("OH", "Ohio"));
            lstItems.Add(new ItemInfo("OK", "Oklahoma"));
            lstItems.Add(new ItemInfo("OR", "Oregon"));
            lstItems.Add(new ItemInfo("PA", "Pennsylvania"));
            lstItems.Add(new ItemInfo("PR", "Puerto Rico"));
            lstItems.Add(new ItemInfo("RI", "Rhode Island"));
            lstItems.Add(new ItemInfo("SC", "South Carolina"));
            lstItems.Add(new ItemInfo("SD", "South Dakota"));
            lstItems.Add(new ItemInfo("TN", "Tennessee"));
            lstItems.Add(new ItemInfo("TX", "Texas"));
            lstItems.Add(new ItemInfo("UT", "Utah"));
            lstItems.Add(new ItemInfo("VT", "Vermont"));
            lstItems.Add(new ItemInfo("VI", "Virgin Islands"));
            lstItems.Add(new ItemInfo("VA", "Virginia"));
            lstItems.Add(new ItemInfo("WA", "Washington"));
            lstItems.Add(new ItemInfo("WV", "West Virginia"));
            lstItems.Add(new ItemInfo("WI", "Wisconsin"));
            lstItems.Add(new ItemInfo("WY", "Wyoming"));

            //Canada provinces
            lstItems.Add(new ItemInfo("AB", "Alberta"));
            lstItems.Add(new ItemInfo("BC", "British Columbia"));
            lstItems.Add(new ItemInfo("MB", "Manitoba"));
            lstItems.Add(new ItemInfo("NB", "New Brunswick"));
            lstItems.Add(new ItemInfo("NL", "Newfoundland and Labrador"));
            lstItems.Add(new ItemInfo("NT", "Northwest Territories"));
            lstItems.Add(new ItemInfo("NS", "Nova Scotia"));
            lstItems.Add(new ItemInfo("NU", "Nunavut"));
            lstItems.Add(new ItemInfo("ON", "Ontario"));
            lstItems.Add(new ItemInfo("PE", "Prince Edward Island"));
            lstItems.Add(new ItemInfo("QC", "Quebec"));
            lstItems.Add(new ItemInfo("SK", "Saskatchewan"));
            lstItems.Add(new ItemInfo("YT", "Yukon"));

            //sort
            lstItems = lstItems.OrderBy(s => s.Name).ToList();

            cbState.Items.AddRange(lstItems.ToArray());
            cbState.ValueMember = "Name";
            cbState.DisplayMember = "Value";
        }

        /// <summary>
        /// Create an EmailCampaign object from user inputs
        /// </summary>
        /// <returns></returns>
        private EmailCampaign CreateCampaignFromInputs()
        {
            EmailCampaign campaign = new EmailCampaign();

            #region General settings

            if (!string.IsNullOrWhiteSpace(txtCampaignName.Text))
            {
                campaign.Name = txtCampaignName.Text.Trim();
            }

            if (cbCampaignType.SelectedItem != null)
            {
                //campaign.TemplateType = GetCampaignType(cbCampaignType.SelectedItem as ItemInfo);
            }

            if (!string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                campaign.Subject = txtSubject.Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(txtFromName.Text))
            {
                campaign.FromName = txtFromName.Text.Trim();
            }

            if (cbFromEmail.SelectedIndex != null)
            {
                campaign.FromEmail = GetSelectedValue(cbFromEmail.SelectedItem as ItemInfo);
            }

            if (cbReplyToEmail.SelectedIndex != null)
            {
                campaign.ReplyToEmail = GetSelectedValue(cbReplyToEmail.SelectedItem as ItemInfo);
            }

            if (rbnHtml.Checked)
            {
                campaign.EmailContentFormat = CampaignEmailFormat.HTML;
            }
            else
            {
                campaign.EmailContentFormat = CampaignEmailFormat.XHTML;
            }

            if (!string.IsNullOrWhiteSpace(txtGreetingString.Text))
            {
                campaign.GreetingString = txtGreetingString.Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(txtEmailContent.Text))
            {
                campaign.EmailContent = txtEmailContent.Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(txtContent.Text))
            {
                campaign.TextContent = txtContent.Text.Trim();
            }

            #endregion General settings

            #region Message footer settings

            //organization name is required for message footer section
            if (!string.IsNullOrWhiteSpace(txtOrganizationName.Text))
            {
                MessageFooter msgFooter = new MessageFooter();

                if (!string.IsNullOrWhiteSpace(txtOrganizationName.Text))
                {
                    msgFooter.OrganizationName = txtOrganizationName.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtAddressLine1.Text))
                {
                    msgFooter.AddressLine1 = txtAddressLine1.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtAddressLine2.Text))
                {
                    msgFooter.AddressLine2 = txtAddressLine2.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtAddressLine3.Text))
                {
                    msgFooter.AddressLine3 = txtAddressLine3.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtCity.Text))
                {
                    msgFooter.City = txtCity.Text.Trim();
                }

                if (!string.IsNullOrWhiteSpace(txtZip.Text))
                {
                    msgFooter.PostalCode = txtZip.Text.Trim();
                }

                if (cbCountry.SelectedItem != null)
                {
                    msgFooter.Country = GetSelectedValue(cbCountry.SelectedItem as ItemInfo);
                }
                if (cbState.SelectedItem != null)
                {
                    var state = cbState.SelectedItem as ItemInfo;
                    if (state != null)
                    {
                        msgFooter.State = state.Name;
                    }
                }


                if (chkIncludeForwardEmail.Checked)
                {
                    msgFooter.ForwardEmailLinkText = txtIncludeForwardEmail.Text.Trim();
                    msgFooter.IncludeForwardEmail = true;
                }

                if (chkIncludeSubscribeLink.Checked)
                {
                    msgFooter.SubscribeLinkText = txtIncludeSubscribeLink.Text.Trim();
                    msgFooter.IncludeSubscribeLink = true;
                }

                campaign.MessageFooter = msgFooter;
            }

            #endregion Message footer settings

            #region Contact lists settings

            List<SentContactList> lists = new List<SentContactList>();

            if (lstContactList.SelectedItems.Count > 0)
            {
                foreach (var item in lstContactList.SelectedItems)
                {
                    SentContactList contactList = new SentContactList() { 
                         Id  = GetSelectedValue(item as ItemInfo)
                    };

                    lists.Add(contactList);
                }
            }

            campaign.Lists = lists;

            #endregion Contact lists settings

            #region Schedule campaign settings

            if (rbnDraft.Checked)
            {
                campaign.Status = CampaignStatus.DRAFT;
            }
            else if (rbnSendNow.Checked)
            {
                campaign.Status = CampaignStatus.SENT;
            }
            else if (rbnScheduled.Checked)
            {
                campaign.Status = CampaignStatus.SCHEDULED;               
            }

            #endregion Schedule campaign settings

            return campaign;
        }

        /// <summary>
        /// Gets the template for campaign based on user selection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private TemplateType GetCampaignType(ItemInfo item)
        {
            if (item != null)
            { 
                switch(item.Value){
                    case "STOCK":
                        return TemplateType.STOCK;
                    default:
                        return TemplateType.CUSTOM;
                }
            }
            return TemplateType.CUSTOM;
        }

        /// <summary>
        /// Gets selected value from a selected item of ItemInfo'type
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetSelectedValue(ItemInfo item)
        {
            if (item != null)
            {
                return item.Value;
            }

            return null;
        }

        /// <summary>
        /// Get details for an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="exceptionType"></param>
        /// <returns></returns>
        private string GetExceptionsDetails(Exception ex, string exceptionType)
        {
            StringBuilder sbExceptions = new StringBuilder();

            sbExceptions.Append(string.Format("{0} thrown:\n", exceptionType));
            sbExceptions.Append(string.Format("Error message: {0}", ex.Message));

            return sbExceptions.ToString();
        }

        #endregion Private methods    

        private void frmEmailCampaign_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// Class used for ComboList controls population
    /// </summary>
    public class ItemInfo
    {
        #region Public members

        public string Name { get; set; }
        public string Value { get; set; }

        #endregion 

        #region Constructor

        public ItemInfo(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion
    }
}
