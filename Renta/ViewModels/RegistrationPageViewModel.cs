﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Renta.Dto_s;
using Renta.enums;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel
    {

        public string password;
        public string email;
        public string FirstName { get; set; }
        public string LastName { get; set;}

        //public ObservableRangeCollection<string> FavoritesCategoriesCollection { get; set; }

        public List<string> Categories { get; set; } = new List<string>();
        public string SelectedCategory1 { get; set; } = string.Empty;
        public string SelectedCategory2 { get; set; } = string.Empty;
        public List<ECategories> SelectedFavoritesCategories { get; set; } = new List<ECategories>();

        public List<string> Regions { get; set; }
        public string SelectedRegion { get; set; }
        public string SelectedCity { get; set; }
        private bool IsFormValid { get; set; } = false;
        private UserService m_userService;
        public RegistrationPageViewModel(UserService userService)
        {
            m_userService = userService;
            password = string.Empty;
            email = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            SelectedCity = string.Empty;
            SelectedRegion = String.Empty;

            FetchRegionsFromString();

            //FavoritesCategoriesCollection = new ObservableRangeCollection<string>();
            //foreach (var value in Enum.GetNames(typeof(ECategories)))
            //{
            //    FavoritesCategoriesCollection.Add(value);
            //}
            //FavoritesCategoriesCollection.RemoveAt(0); // removes the ALL

           
            foreach (var value in Enum.GetNames(typeof(ECategories)))
            {
                Categories.Add(value);
            }
            Categories.RemoveAt(0);


            // var test1 = FileSystem.Current.AppDataDirectory;

            //var citiesFile = File.ReadAllLines(@"e:\Users\Dan\Desktop\Renta UI\Renta\Renta\Resources\cities.txt");
            //var Cities = new List<string>(citiesFile);

        }



        //public async Task FetchCities()
        //{

        //    //using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("cities");
        //    //using StreamReader reader = new StreamReader(fileStream);

        //    //var str1  = reader.ReadToEndAsync();
        //    //string filePath = "cities.txt";
        //    //var stream = await FileSystem.OpenAppPackageFileAsync("Assets/" + filePath);
        //}


        public string Password
        {
            set
            {
                password = value;
                OnPropertyChanged();
            }
            get => password;
        }
        public string Email
        {
            set
            {
                email = value;
                OnPropertyChanged();
            }
            get => email;
        }


     




        public Command RegisterCompleted_Clicked
         => new Command(async () => await registerUser());

        private async Task registerUser()
        {
            if(SelectedCategory1 != string.Empty)
            {
                SelectedFavoritesCategories.Add( Enum.Parse<ECategories>(SelectedCategory1));
            }
            if (SelectedCategory2 != string.Empty)
            {
                SelectedFavoritesCategories.Add(Enum.Parse<ECategories>(SelectedCategory2));
            }


            if (password.Length > 0 && email.Length > 0  && FirstName.Length > 0 && LastName.Length > 0 && SelectedRegion.Length > 0)
            {
                RegisterDto registerDto = new RegisterDto(email, password, FirstName, LastName, SelectedCity, SelectedRegion, SelectedFavoritesCategories);
                await m_userService.RegisterUser(registerDto);

                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }



        private void FetchRegionsFromString()
        {
            //string RegionsStr = "A'SAM \r\nABBIRIM \r\nABU ABDUN \r\nABU AMMAR \r\nABU AMRE \r\nABU GHOSH \r\nABU JUWEI'ID \r\nABU QUREINAT \r\nABU QUREINAT \r\nABU RUBEI'A \r\nABU RUQAYYEQ \r\nABU SINAN \r\nABU SUREIHAN \r\nABU TULUL \r\nADAMIT \r\nADANIM \r\nADDERET \r\nADDIRIM \r\nADI \r\nADORA \r\nAFEINISH \r\nAFEQ \r\nAFIQ \r\nAFIQIM \r\nAFULA \r\nAGUR \r\nAHAWA \r\nAHI'EZER \r\nAHIHUD \r\nAHISAMAKH \r\nAHITUV \r\nAHUZZAM \r\nAHUZZAT BARAQ \r\nAKKO \r\nAL SAYYID \r\nAL-ARYAN \r\nAL-AZY \r\nALE ZAHAV \r\nALFE MENASHE \r\nALLON HAGALIL \r\nALLON SHEVUT \r\nALLONE ABBA \r\nALLONE HABASHAN \r\nALLONE YIZHAQ \r\nALLONIM \r\nALMA \r\nALMAGOR \r\nALMOG \r\nALMON \r\nALUMIM \r\nALUMMA \r\nALUMMOT \r\nAMAZYA \r\nAMIR \r\nAMIRIM \r\nAMMI'AD \r\nAMMI'OZ \r\nAMMIHAY \r\nAMMINADAV \r\nAMMIQAM \r\nAMNUN \r\nAMQA \r\nAMUQQA \r\nANI'AM \r\nAR'ARA \r\nAR'ARA-BANEGEV \r\nARAD \r\nARAMSHA \r\nARBEL \r\nARGAMAN \r\nARI'EL \r\nARRAB AL NAIM \r\nARRABE \r\nARSUF \r\nARUGOT \r\nASAD \r\nASEFAR \r\nASERET \r\nASHALIM \r\nASHDOD \r\nASHDOT YA'AQOV(IHUD) \r\nASHDOT YA'AQOV(ME'UH \r\nASHERAT \r\nASHQELON \r\nATAWNE \r\nATERET \r\nATLIT \r\nATRASH \r\nATSMON-SEGEV \r\nAVDON \r\nAVENAT \r\nAVI'EL \r\nAVI'EZER \r\nAVIGEDOR \r\nAVIHAYIL \r\nAVITAL \r\nAVIVIM \r\nAVNE ETAN \r\nAVNE HEFEZ \r\nAVSHALOM \r\nAVTALYON \r\nAYANOT \r\nAYYELET HASHAHAR \r\nAZARYA \r\nAZOR \r\nAZRI'EL \r\nAZRIQAM \r\nBAHAN \r\nBALFURIYYA \r\nBAQA AL-GHARBIYYE \r\nBAR GIYYORA \r\nBAR YOHAY \r\nBAR'AM \r\nBARAQ \r\nBAREQET \r\nBARQAN \r\nBARQAY \r\nBASMA \r\nBASMAT TAB'UN \r\nBAT AYIN \r\nBAT HADAR \r\nBAT HEFER \r\nBAT HEN \r\nBAT SHELOMO \r\nBAT YAM \r\nBAZRA \r\nBE'ER MILKA \r\nBE'ER ORA \r\nBE'ER SHEVA \r\nBE'ER TUVEYA \r\nBE'ER YA'AQOV \r\nBE'ERI \r\nBE'EROT YIZHAQ \r\nBE'EROTAYIM \r\nBEER GANNIM \r\nBEIT JANN \r\nBEN AMMI \r\nBEN SHEMEN (MOSHAV) \r\nBEN SHEMEN(K.NO'AR) \r\nBEN ZAKKAY \r\nBENAYA \r\nBENE ATAROT \r\nBENE AYISH \r\nBENE BERAQ \r\nBENE DAROM \r\nBENE DEROR \r\nBENE NEZARIM \r\nBENE RE'EM \r\nBENE YEHUDA \r\nBENE ZIYYON \r\nBEQA'OT \r\nBEQOA \r\nBERAKHA \r\nBEREKHYA \r\nBEROR HAYIL \r\nBEROSH \r\nBET ALFA \r\nBET ARIF \r\nBET ARYE \r\nBET BERL \r\nBET DAGAN \r\nBET EL \r\nBET EL'AZARI \r\nBET EZRA \r\nBET GAMLI'EL \r\nBET GUVRIN \r\nBET HAARAVA \r\nBET HAEMEQ \r\nBET HAGADDI \r\nBET HALEVI \r\nBET HANAN \r\nBET HANANYA \r\nBET HASHITTA \r\nBET HASHMONAY \r\nBET HERUT \r\nBET HILLEL \r\nBET HILQIYYA \r\nBET HORON \r\nBET LEHEM HAGELILIT \r\nBET ME'IR \r\nBET NEHEMYA \r\nBET NEQOFA \r\nBET NIR \r\nBET OREN \r\nBET OVED \r\nBET QAMA \r\nBET QESHET \r\nBET RABBAN \r\nBET RIMMON \r\nBET SHE'AN \r\nBET SHE'ARIM \r\nBET SHEMESH \r\nBET SHIQMA \r\nBET UZZI'EL \r\nBET YANNAY \r\nBET YEHOSHUA \r\nBET YIZHAQ-SH. HEFER \r\nBET YOSEF \r\nBET ZAYIT \r\nBET ZEID \r\nBET ZERA \r\nBET ZEVI \r\nBETAR ILLIT \r\nBEZET \r\nBI NE \r\nBINYAMINA \r\nBIR EL-MAKSUR \r\nBIR HADAGE \r\nBIRIYYA \r\nBITAN AHARON \r\nBITHA \r\nBIZZARON \r\nBNE DKALIM \r\nBRUKHIN \r\nBU'EINE-NUJEIDAT \r\nBUQ'ATA \r\nBURGETA \r\nBUSTAN HAGALIL \r\nDABBURYE \r\nDAFNA \r\nDAHI \r\nDALIYAT AL-KARMEL \r\nDALIYYA \r\nDALTON \r\nDAN \r\nDAVERAT \r\nDEGANYA ALEF \r\nDEGANYA BET \r\nDEIR AL-ASAD \r\nDEIR HANNA \r\nDEIR RAFAT \r\nDEMEIDE \r\nDEQEL \r\nDERIG'AT \r\nDEVIRA \r\nDEVORA \r\nDIMONA \r\nDISHON \r\nDOLEV \r\nDOR \r\nDOROT \r\nDOVEV \r\nEFRAT \r\nEILABUN \r\nEIN AL-ASAD \r\nEIN HOD \r\nEIN MAHEL \r\nEIN NAQQUBA \r\nEIN QINIYYE \r\nEIN RAFA \r\nEL'AD \r\nEL'AZAR \r\nEL-ROM \r\nELAT \r\nELI \r\nELI AL \r\nELIAV \r\nELIFAZ \r\nELIFELET \r\nELISHAMA \r\nELON \r\nELON MORE \r\nELOT \r\nELQANA \r\nELQOSH \r\nELYAKHIN \r\nELYAQIM \r\nELYASHIV \r\nEMUNIM \r\nEN AYYALA \r\nEN DOR \r\nEN GEDI \r\nEN GEV \r\nEN HABESOR \r\nEN HAEMEQ \r\nEN HAHORESH \r\nEN HAMIFRAZ \r\nEN HANAZIV \r\nEN HAROD (IHUD) \r\nEN HAROD(ME'UHAD) \r\nEN HASHELOSHA \r\nEN HASHOFET \r\nEN HAZEVA \r\nEN HOD \r\nEN IRON \r\nEN KAREM-B.S.HAQLA'I \r\nEN KARMEL \r\nEN SARID \r\nEN SHEMER \r\nEN TAMAR \r\nEN WERED \r\nEN YA'AQOV \r\nEN YAHAV \r\nEN ZIWAN \r\nEN ZURIM \r\nENAT \r\nENAV \r\nEREZ \r\nESHBOL \r\nESHEL HANASI \r\nESHHAR \r\nESHKOLOT \r\nESHTA'OL \r\nETAN \r\nETANIM \r\nETGAR \r\nEVEN MENAHEM \r\nEVEN SAPPIR \r\nEVEN SHEMU'EL \r\nEVEN YEHUDA \r\nEVEN YIZHAQ(GAL'ED) \r\nEVRON \r\nEYAL \r\nEZER \r\nEZUZ \r\nFASSUTA \r\nFUREIDIS \r\nGA'ASH \r\nGA'TON \r\nGADISH \r\nGADOT \r\nGAL'ON \r\nGAN HADAROM \r\nGAN HASHOMERON \r\nGAN HAYYIM \r\nGAN NER \r\nGAN SHELOMO \r\nGAN SHEMU'EL \r\nGAN SOREQ \r\nGAN YAVNE \r\nGAN YOSHIYYA \r\nGANNE AM \r\nGANNE HADAR \r\nGANNE MODIIN \r\nGANNE TAL \r\nGANNE TIQWA \r\nGANNE YOHANAN \r\nGANNOT \r\nGANNOT HADAR \r\nGAT RIMMON \r\nGAT(QIBBUZ) \r\nGAZIT \r\nGE'A \r\nGE'ALYA \r\nGE'ULE TEMAN \r\nGE'ULIM \r\nGEDERA \r\nGEFEN \r\nGELIL YAM \r\nGEROFIT \r\nGESHER \r\nGESHER HAZIW \r\nGESHUR \r\nGEVA \r\nGEVA KARMEL \r\nGEVA BINYAMIN \r\nGEVA'OT BAR \r\nGEVAR'AM \r\nGEVAT \r\nGEVIM \r\nGEVULOT \r\nGEZER \r\nGHAJAR \r\nGIBBETON \r\nGID'ONA \r\nGILAT \r\nGILGAL \r\nGILON \r\nGIMZO \r\nGINNATON \r\nGINNEGAR \r\nGINNOSAR \r\nGITTA \r\nGITTIT \r\nGIV'AT AVNI \r\nGIV'AT BRENNER \r\nGIV'AT ELA \r\nGIV'AT HASHELOSHA \r\nGIV'AT HAYYIM (IHUD) \r\nGIV'AT HAYYIM(ME'UHA \r\nGIV'AT HEN \r\nGIV'AT KOAH \r\nGIV'AT NILI \r\nGIV'AT OZ \r\nGIV'AT SHAPPIRA \r\nGIV'AT SHEMESH \r\nGIV'AT SHEMU'EL \r\nGIV'AT YE'ARIM \r\nGIV'AT YESHA'YAHU \r\nGIV'AT YO'AV \r\nGIV'AT ZE'EV \r\nGIV'ATAYIM \r\nGIV'ATI \r\nGIV'OLIM \r\nGIV'ON HAHADASHA \r\nGIV'OT EDEN \r\nGIZO \r\nGONEN \r\nGOREN \r\nGORNOT HAGALIL \r\nHABONIM \r\nHAD-NES \r\nHADAR AM \r\nHADERA \r\nHADID \r\nHAFEZ HAYYIM \r\nHAGGAI \r\nHAGOR \r\nHAGOSHERIM \r\nHAHOTERIM \r\nHAIFA \r\nHALUZ \r\nHAMA'PIL \r\nHAMADYA \r\nHAMAM \r\nHAMRA \r\nHANITA \r\nHANNATON \r\nHANNI'EL \r\nHAOGEN \r\nHAON \r\nHAR ADAR \r\nHAR AMASA \r\nHAR GILLO \r\nHAR'EL \r\nHARARIT \r\nHARASHIM \r\nHARDUF \r\nHARISH \r\nHARUZIM \r\nHASHMONA'IM \r\nHASOLELIM \r\nHASPIN \r\nHAVAZZELET HASHARON \r\nHAWASHLA \r\nHAYOGEV \r\nHAZAV \r\nHAZERIM \r\nHAZEVA \r\nHAZON \r\nHAZOR HAGELILIT \r\nHAZOR-ASHDOD \r\nHAZORE'IM \r\nHAZOREA \r\nHEFZI-BAH \r\nHELEZ \r\nHEMED \r\nHEREV LE'ET \r\nHERMESH \r\nHERUT \r\nHERZELIYYA \r\nHEVER \r\nHIBBAT ZIYYON \r\nHILLA \r\nHINNANIT \r\nHOD HASHARON \r\nHODAYOT \r\nHODIYYA \r\nHOFIT \r\nHOGLA \r\nHOLIT \r\nHOLON \r\nHORESHIM \r\nHOSEN \r\nHOSHA'AYA \r\nHUJEIRAT (DAHRA) \r\nHULATA \r\nHULDA \r\nHUQOQ \r\nHURA \r\nHURFEISH \r\nHUSSNIYYA \r\nHUZAYYEL \r\nI'BILLIN \r\nIBBIM \r\nIBTIN \r\nIDDAN \r\nIKSAL \r\nILANIYYA \r\nILUT \r\nIMMANU'EL \r\nIR OVOT \r\nIRUS \r\nISIFYA \r\nITAMAR \r\nJAAT \r\nJALJULYE \r\nJERUSALEM \r\nJISH(GUSH HALAV) \r\nJISR AZ-ZARQA \r\nJUDEIDE-MAKER \r\nJULIS \r\nJUNNABIB \r\nKA'ABIYYE-TABBASH-HA \r\nKABRI \r\nKABUL \r\nKADDITA \r\nKADOORIE \r\nKAFAR BARA \r\nKAFAR KAMA \r\nKAFAR KANNA \r\nKAFAR MANDA \r\nKAFAR MISR \r\nKAFAR QARA \r\nKAFAR QASEM \r\nKAFAR YASIF \r\nKAHAL \r\nKALLANIT \r\nKAMMON \r\nKANAF \r\nKANNOT \r\nKAOKAB ABU AL-HIJA \r\nKARE DESHE \r\nKARKOM \r\nKARME QATIF \r\nKARME YOSEF \r\nKARME ZUR \r\nKARMEL \r\nKARMI'EL \r\nKARMIYYA \r\nKEFAR ADUMMIM \r\nKEFAR AHIM \r\nKEFAR AVIV \r\nKEFAR AVODA \r\nKEFAR AZZA \r\nKEFAR BARUKH \r\nKEFAR BIALIK \r\nKEFAR BILU \r\nKEFAR BIN NUN \r\nKEFAR BLUM \r\nKEFAR DANIYYEL \r\nKEFAR EZYON \r\nKEFAR GALLIM \r\nKEFAR GID'ON \r\nKEFAR GIL'ADI \r\nKEFAR GLIKSON \r\nKEFAR HABAD \r\nKEFAR HAHORESH \r\nKEFAR HAMAKKABI \r\nKEFAR HANAGID \r\nKEFAR HANANYA \r\nKEFAR HANASI \r\nKEFAR HANO'AR HADATI \r\nKEFAR HAORANIM \r\nKEFAR HARIF \r\nKEFAR HARO'E \r\nKEFAR HARUV \r\nKEFAR HASIDIM ALEF \r\nKEFAR HASIDIM BET \r\nKEFAR HAYYIM \r\nKEFAR HESS \r\nKEFAR HITTIM \r\nKEFAR HOSHEN \r\nKEFAR KISH \r\nKEFAR MALAL \r\nKEFAR MASARYK \r\nKEFAR MAYMON \r\nKEFAR MENAHEM \r\nKEFAR MONASH \r\nKEFAR MORDEKHAY \r\nKEFAR NETTER \r\nKEFAR PINES \r\nKEFAR ROSH HANIQRA \r\nKEFAR ROZENWALD(ZAR. \r\nKEFAR RUPPIN \r\nKEFAR RUT \r\nKEFAR SAVA \r\nKEFAR SHAMMAY \r\nKEFAR SHEMARYAHU \r\nKEFAR SHEMU'EL \r\nKEFAR SILVER \r\nKEFAR SIRKIN \r\nKEFAR SZOLD \r\nKEFAR TAPPUAH \r\nKEFAR TAVOR \r\nKEFAR TRUMAN \r\nKEFAR URIYYA \r\nKEFAR VITKIN \r\nKEFAR WARBURG \r\nKEFAR WERADIM \r\nKEFAR YA'BEZ \r\nKEFAR YEHEZQEL \r\nKEFAR YEHOSHUA \r\nKEFAR YONA \r\nKEFAR ZETIM \r\nKEFAR ZOHARIM \r\nKELIL \r\nKEMEHIN \r\nKERAMIM \r\nKEREM BEN SHEMEN \r\nKEREM BEN ZIMRA \r\nKEREM MAHARAL \r\nKEREM SHALOM \r\nKEREM YAVNE(YESHIVA) \r\nKESALON \r\nKHAWALED \r\nKHAWALED \r\nKINNERET(MOSHAVA) \r\nKINNERET(QEVUZA) \r\nKISHOR \r\nKISRA-SUMEI \r\nKISSUFIM \r\nKOCHLEA \r\nKOKHAV HASHAHAR \r\nKOKHAV MIKHA'EL \r\nKOKHAV YA'AQOV \r\nKOKHAV YA'IR \r\nKORAZIM \r\nKUSEIFE \r\nLAHAV \r\nLAHAVOT HABASHAN \r\nLAHAVOT HAVIVA \r\nLAKHISH \r\nLAPPID \r\nLAPPIDOT \r\nLAQYE \r\nLAVI \r\nLAVON \r\nLEHAVIM \r\nLI-ON \r\nLIMAN \r\nLIVNIM \r\nLOD \r\nLOHAME HAGETA'OT \r\nLOTAN \r\nLOTEM \r\nLUZIT \r\nMA'AGAN \r\nMA'AGAN MIKHA'EL \r\nMA'ALE ADUMMIM \r\nMA'ALE AMOS \r\nMA'ALE EFRAYIM \r\nMA'ALE GAMLA \r\nMA'ALE GILBOA \r\nMA'ALE HAHAMISHA \r\nMA'ALE IRON \r\nMA'ALE LEVONA \r\nMA'ALE MIKHMAS \r\nMA'ALOT-TARSHIHA \r\nMA'ANIT \r\nMA'AS \r\nMA'BAROT \r\nMA'GALIM \r\nMA'ON \r\nMA'OR \r\nMA'OZ HAYYIM \r\nMA'YAN BARUKH \r\nMA'YAN ZEVI \r\nMABBU'IM \r\nMAGEN \r\nMAGEN SHA'UL \r\nMAGGAL \r\nMAGSHIMIM \r\nMAHANAYIM \r\nMAHANE BILDAD \r\nMAHANE HILLA \r\nMAHANE MIRYAM \r\nMAHANE TALI \r\nMAHANE TEL NOF \r\nMAHANE YAFA \r\nMAHANE YATTIR \r\nMAHANE YEHUDIT \r\nMAHANE YOKHVED \r\nMAHSEYA \r\nMAJD AL-KURUM \r\nMAJDAL SHAMS \r\nMAKCHUL \r\nMALKISHUA \r\nMALKIYYA \r\nMANOF \r\nMANOT \r\nMANSHIYYET ZABDA \r\nMARGALIYYOT \r\nMAS'ADE \r\nMAS'UDIN AL-'AZAZME \r\nMASH'ABBE SADE \r\nMASH'EN \r\nMASKIYYOT \r\nMASLUL \r\nMASSAD \r\nMASSADA \r\nMASSU'A \r\nMASSUOT YIZHAQ \r\nMATTA \r\nMATTAN \r\nMATTAT \r\nMATTITYAHU \r\nMAVQI'IM \r\nMAZKERET BATYA \r\nMAZLIAH \r\nMAZOR \r\nMAZRA'A \r\nMAZZUVA \r\nME AMMI \r\nME'IR SHEFEYA \r\nME'ONA \r\nMEFALLESIM \r\nMEGADIM \r\nMEGIDDO \r\nMEHOLA \r\nMEISER \r\nMEKHORA \r\nMELE'A \r\nMELILOT \r\nMENAHEMYA \r\nMENNARA \r\nMENUHA \r\nMERAV \r\nMERHAV AM \r\nMERHAVYA(MOSHAV) \r\nMERHAVYA(QIBBUZ) \r\nMERKAZ SHAPPIRA \r\nMEROM GOLAN \r\nMERON \r\nMESHAR \r\nMESHHED \r\nMESILLAT ZIYYON \r\nMESILLOT \r\nMETAR \r\nMETAV \r\nMETULA \r\nMEVASSERET ZIYYON \r\nMEVO BETAR \r\nMEVO DOTAN \r\nMEVO HAMMA \r\nMEVO HORON \r\nMEVO MODI'IM \r\nMEVO'OT YAM \r\nMEVO'OT YERIHO \r\nMEZADOT YEHUDA \r\nMEZAR \r\nMEZER \r\nMI'ELYA \r\nMIDRAKH OZ \r\nMIDRESHET BEN GURION \r\nMIDRESHET RUPPIN \r\nMIGDAL \r\nMIGDAL HAEMEQ \r\nMIGDAL OZ \r\nMIGDALIM \r\nMIKHMANNIM \r\nMIKHMORET \r\nMIQWE YISRA'EL \r\nMISGAV AM \r\nMISGAV DOV \r\nMISHMAR AYYALON \r\nMISHMAR DAWID \r\nMISHMAR HAEMEQ \r\nMISHMAR HANEGEV \r\nMISHMAR HASHARON \r\nMISHMAR HASHIV'A \r\nMISHMAR HAYARDEN \r\nMISHMAROT \r\nMISHMERET \r\nMITSPE ILAN \r\nMIVTAHIM \r\nMIZPA \r\nMIZPE AVIV \r\nMIZPE NETOFA \r\nMIZPE RAMON \r\nMIZPE SHALEM \r\nMIZPE YERIHO \r\nMIZRA \r\nMODI'IN ILLIT \r\nMODI'IN-MAKKABBIM-RE \r\nMOLEDET \r\nMORAN \r\nMORESHET \r\nMOZA ILLIT \r\nMUGHAR \r\nMUQEIBLE \r\nNA'ALE \r\nNA'AN \r\nNA'ARAN \r\nNA'URA \r\nNAAMA \r\nNAHAL ESHBAL \r\nNAHAL HEMDAT \r\nNAHAL OZ \r\nNAHAL SHITTIM \r\nNAHALA \r\nNAHALAL \r\nNAHALI'EL \r\nNAHAM \r\nNAHARIYYA \r\nNAHEF \r\nNAHSHOLIM \r\nNAHSHON \r\nNAHSHONIM \r\nNASASRA \r\nNATAF \r\nNATUR \r\nNAVE \r\nNAZARETH \r\nNE'OT GOLAN \r\nNE'OT HAKIKKAR \r\nNE'OT MORDEKHAY \r\nNE'URIM \r\nNEGBA \r\nNEGOHOT \r\nNEHALIM \r\nNEHORA \r\nNEHUSHA \r\nNEIN \r\nNES AMMIM \r\nNES HARIM \r\nNES ZIYYONA \r\nNESHER \r\nNETA \r\nNETA'IM \r\nNETANYA \r\nNETIV HAASARA \r\nNETIV HAGEDUD \r\nNETIV HALAMED-HE \r\nNETIV HASHAYYARA \r\nNETIVOT \r\nNETU'A \r\nNEVATIM \r\nNEVE TSUF \r\nNEWE ATIV \r\nNEWE AVOT \r\nNEWE DANIYYEL \r\nNEWE ETAN \r\nNEWE HARIF \r\nNEWE ILAN \r\nNEWE MIKHA'EL \r\nNEWE MIVTAH \r\nNEWE SHALOM \r\nNEWE UR \r\nNEWE YAM \r\nNEWE YAMIN \r\nNEWE YARAQ \r\nNEWE ZIV \r\nNEWE ZOHAR \r\nNEZER HAZZANI \r\nNEZER SERENI \r\nNILI \r\nNIMROD \r\nNIR AM \r\nNIR AQIVA \r\nNIR BANIM \r\nNIR DAWID (TEL AMAL) \r\nNIR ELIYYAHU \r\nNIR EZYON \r\nNIR GALLIM \r\nNIR HEN \r\nNIR MOSHE \r\nNIR OZ \r\nNIR YAFE \r\nNIR YISRA'EL \r\nNIR YIZHAQ \r\nNIR ZEVI \r\nNIRIM \r\nNIRIT \r\nNIZZAN \r\nNIZZAN B \r\nNIZZANA (QEHILAT HIN \r\nNIZZANE OZ \r\nNIZZANE SINAY \r\nNIZZANIM \r\nNO'AM \r\nNOF AYYALON \r\nNOF HAGALIL \r\nNOFEKH \r\nNOFIM \r\nNOFIT \r\nNOGAH \r\nNOQEDIM \r\nNORDIYYA \r\nNOV \r\nNURIT \r\nODEM \r\nOFAQIM \r\nOFER \r\nOFRA \r\nOHAD \r\nOLESH \r\nOMEN \r\nOMER \r\nOMEZ \r\nOR AQIVA \r\nOR HAGANUZ \r\nOR HANER \r\nOR YEHUDA \r\nORA \r\nORANIM \r\nORANIT \r\nOROT \r\nORTAL \r\nOTNI'EL \r\nOZEM \r\nPA'AME TASHAZ \r\nPALMAHIM \r\nPARAN \r\nPARDES HANNA-KARKUR \r\nPARDESIYYA \r\nPAROD \r\nPATTISH \r\nPEDAYA \r\nPEDU'EL \r\nPEDUYIM \r\nPELEKH \r\nPENE HEVER \r\nPEQI'IN (BUQEI'A) \r\nPEQI'IN HADASHA \r\nPERAZON \r\nPERI GAN \r\nPESAGOT \r\nPETAH TIQWA \r\nPETAHYA \r\nPEZA'EL \r\nPORAT \r\nPORIYYA ILLIT \r\nPORIYYA-KEFAR AVODA \r\nPORIYYA-NEWE OVED \r\nQABBO'A \r\nQADDARIM \r\nQADIMA-ZORAN \r\nQALANSAWE \r\nQALYA \r\nQARNE SHOMERON \r\nQAWA'IN \r\nQAZIR \r\nQAZRIN \r\nQEDAR \r\nQEDMA \r\nQEDUMIM \r\nQELA \r\nQELAHIM \r\nQESARIYYA \r\nQESHET \r\nQETURA \r\nQEVUZAT YAVNE \r\nQIDMAT ZEVI \r\nQIDRON \r\nQIRYAT ANAVIM \r\nQIRYAT ARBA \r\nQIRYAT ATTA \r\nQIRYAT BIALIK \r\nQIRYAT EQRON \r\nQIRYAT GAT \r\nQIRYAT MAL'AKHI \r\nQIRYAT MOTZKIN \r\nQIRYAT NETAFIM \r\nQIRYAT ONO \r\nQIRYAT SHELOMO \r\nQIRYAT SHEMONA \r\nQIRYAT TIV'ON \r\nQIRYAT YAM \r\nQIRYAT YE'ARIM \r\nQIRYAT YE'ARIM(INSTI \r\nQOMEMIYYUT \r\nQORANIT \r\nQUDEIRAT AS-SANI \r\nRA'ANANA \r\nRAHAT \r\nRAM-ON \r\nRAMAT DAWID \r\nRAMAT GAN \r\nRAMAT HAKOVESH \r\nRAMAT HASHARON \r\nRAMAT HASHOFET \r\nRAMAT MAGSHIMIM \r\nRAMAT RAHEL \r\nRAMAT RAZI'EL \r\nRAMAT YISHAY \r\nRAMAT YOHANAN \r\nRAMAT ZEVI \r\nRAME \r\nRAMLA \r\nRAMOT \r\nRAMOT HASHAVIM \r\nRAMOT ME'IR \r\nRAMOT MENASHE \r\nRAMOT NAFTALI \r\nRANNEN \r\nRAQEFET \r\nRAS AL-EIN \r\nRAS ALI \r\nRAVID \r\nRE'IM \r\nREGAVIM \r\nREGBA \r\nREHAN \r\nREHELIM \r\nREHOV \r\nREHOVOT \r\nREIHANIYYE \r\nREINE \r\nREKHASIM \r\nRESHAFIM \r\nRETAMIM \r\nREVADIM \r\nREVAVA \r\nREVIVIM \r\nREWAHA \r\nREWAYA \r\nRIMMONIM \r\nRINNATYA \r\nRISHON LEZIYYON \r\nRISHPON \r\nRO'I \r\nROSH HAAYIN \r\nROSH PINNA \r\nROSH ZURIM \r\nROTEM \r\nRUAH MIDBAR \r\nRUHAMA \r\nRUMAT HEIB \r\nRUMMANE \r\nSA'AD \r\nSA'AR \r\nSA'WA \r\nSAJUR \r\nSAKHNIN \r\nSAL'IT \r\nSALLAMA \r\nSAMAR \r\nSANDALA \r\nSAPPIR \r\nSARID \r\nSASA \r\nSAVYON \r\nSAWA'ID (KAMANE) \r\nSAWA'ID(HAMRIYYE) \r\nSAYYID \r\nSEDE AVRAHAM \r\nSEDE BOQER \r\nSEDE DAWID \r\nSEDE ELI'EZER \r\nSEDE ELIYYAHU \r\nSEDE HEMED \r\nSEDE ILAN \r\nSEDE MOSHE \r\nSEDE NAHUM \r\nSEDE NEHEMYA \r\nSEDE NIZZAN \r\nSEDE TERUMOT \r\nSEDE UZZIYYAHU \r\nSEDE WARBURG \r\nSEDE YA'AQOV \r\nSEDE YIZHAQ \r\nSEDE YO'AV \r\nSEDE ZEVI \r\nSEDEROT \r\nSEDOT MIKHA \r\nSEDOT YAM \r\nSEGEV-SHALOM \r\nSEGULA \r\nSENIR \r\nSHA'AB \r\nSHA'AL \r\nSHA'ALVIM \r\nSHA'AR EFRAYIM \r\nSHA'AR HAAMAQIM \r\nSHA'AR HAGOLAN \r\nSHA'AR MENASHE \r\nSHADMOT DEVORA \r\nSHADMOT MEHOLA \r\nSHAFIR \r\nSHAHAR \r\nSHAHARUT \r\nSHALVA BAMIDBAR \r\nSHALWA \r\nSHAMERAT \r\nSHAMIR \r\nSHANI \r\nSHAQED \r\nSHARONA \r\nSHARSHERET \r\nSHAVE DAROM \r\nSHAVE SHOMERON \r\nSHAVE ZIYYON \r\nSHE'AR YASHUV \r\nSHEDEMA \r\nSHEFAR'AM \r\nSHEFAYIM \r\nSHEFER \r\nSHEIKH DANNUN \r\nSHEKHANYA \r\nSHELOMI \r\nSHELUHOT \r\nSHEQEF \r\nSHETULA \r\nSHETULIM \r\nSHEZAF \r\nSHEZOR \r\nSHIBBOLIM \r\nSHIBLI \r\nSHILAT \r\nSHILO \r\nSHIM'A \r\nSHIMSHIT \r\nSHIZZAFON \r\nSHLOMIT \r\nSHO'EVA \r\nSHOHAM \r\nSHOMERA \r\nSHOMERIYYA \r\nSHOQEDA \r\nSHORASHIM \r\nSHORESH \r\nSHOSHANNAT HAAMAQIM \r\nSHOSHANNAT HAAMAQIM( \r\nSHOVAL \r\nSHUVA \r\nSITRIYYA \r\nSUFA \r\nSULAM \r\nSUSEYA \r\nTA'OZ \r\nTAL SHAHAR \r\nTAL-EL \r\nTALME BILU \r\nTALME EL'AZAR \r\nTALME ELIYYAHU \r\nTALME YAFE \r\nTALME YEHI'EL \r\nTALME YOSEF \r\nTALMON \r\nTAMRA \r\nTAMRA (YIZRE'EL) \r\nTARABIN AS-SANI \r\nTARABIN AS-SANI \r\nTARUM \r\nTAYIBE \r\nTAYIBE(BAEMEQ) \r\nTE'ASHUR \r\nTEFAHOT \r\nTEL ADASHIM \r\nTEL AVIV - YAFO \r\nTEL MOND \r\nTEL QAZIR \r\nTEL SHEVA \r\nTEL TE'OMIM \r\nTEL YIZHAQ \r\nTEL YOSEF \r\nTELALIM \r\nTELAMIM \r\nTELEM \r\nTENE \r\nTENUVOT \r\nTEQOA \r\nTEQUMA \r\nTIBERIAS \r\nTIDHAR \r\nTIFRAH \r\nTIMMORIM \r\nTIMRAT \r\nTIRAT KARMEL \r\nTIRAT YEHUDA \r\nTIRAT ZEVI \r\nTIRE \r\nTIROSH \r\nTOMER \r\nTRUMP HEIGHTS \r\nTUBA-ZANGARIYYE \r\nTUR'AN \r\nTUSHIYYA \r\nTUVAL \r\nUDIM \r\nUMM AL-FAHM \r\nUMM AL-QUTUF \r\nUMM BATIN \r\nUQBI (BANU UQBA) \r\nURIM \r\nUSHA \r\nUZA \r\nUZEIR \r\nWARDON \r\nWERED YERIHO \r\nYA'AD \r\nYA'ARA \r\nYA'EL \r\nYAD BINYAMIN \r\nYAD HANNA \r\nYAD HASHEMONA \r\nYAD MORDEKHAY \r\nYAD NATAN \r\nYAD RAMBAM \r\nYAFI \r\nYAFIT \r\nYAGEL \r\nYAGUR \r\nYAHEL \r\nYAKHINI \r\nYANUH-JAT \r\nYANUV \r\nYAQIR \r\nYAQUM \r\nYARDENA \r\nYARHIV \r\nYARQONA \r\nYAS'UR \r\nYASHRESH \r\nYATED \r\nYAVNE \r\nYAVNE'EL \r\nYAZIZ \r\nYE'AF \r\nYEDIDA \r\nYEDIDYA \r\nYEHI'AM \r\nYEHUD-MONOSON \r\nYEROHAM \r\nYESHA \r\nYESODOT \r\nYESUD HAMA'ALA \r\nYEVUL \r\nYIF'AT \r\nYIFTAH \r\nYINNON \r\nYIR'ON \r\nYIRKA \r\nYISH'I \r\nYITAV \r\nYIZHAR \r\nYIZRE'EL \r\nYODEFAT \r\nYONATAN \r\nYOQNE'AM ILLIT \r\nYOQNE'AM(MOSHAVA) \r\nYOSHIVYA \r\nYOTVATA \r\nYUVAL \r\nYUVALIM \r\nZABARGA \r\nZAFRIRIM \r\nZAFRIYYA \r\nZANOAH \r\nZARZIR \r\nZAVDI'EL \r\nZE'ELIM \r\nZEFAT \r\nZEKHARYA \r\nZELAFON \r\nZEMER \r\nZERAHYA \r\nZERU'A \r\nZERUFA \r\nZETAN \r\nZIKHRON YA'AQOV \r\nZIMRAT \r\nZIPPORI \r\nZIQIM \r\nZIV'ON \r\nZOFAR \r\nZOFIT \r\nZOFIYYA \r\nZOHAR \r\nZOHAR \r\nZOR'A \r\nZOVA \r\nZUFIN \r\nZUR HADASSA \r\nZUR MOSHE \r\nZUR NATAN \r\nZUR YIZHAQ \r\nZURI'EL \r\nZURIT \r\nZVIYYA";
            string RegionsStr = "Ashkelon \r\nBeer Sheva \r\nBethlehem \r\nGolan \r\nJenin \r\nHasharon \r\nHebron \r\nHadera \r\nHolon \r\nHaifa \r\nTulkarm \r\nJericho \r\nJerusalem \r\nKinneret \r\nNazareth \r\nAcre \r\nAfula \r\nPetah Tikva \r\nSafed \r\nRamallah \r\nRehovot \r\nRamla \r\nRamat Gan \r\nNablus \r\nTel Aviv";
            var stringArr = RegionsStr.Split("\r\n");
            Regions = stringArr.ToList();
        }
    }
}