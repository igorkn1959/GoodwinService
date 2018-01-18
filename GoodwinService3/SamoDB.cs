using System;
namespace GoodwinService3 {
    
    
    public partial class SamoDB {
        

        public void FillHotelPacket(byte[] catClaim)
        {
            
            SamoDBTableAdapters.PacketHotelTableAdapter TA = new SamoDBTableAdapters.PacketHotelTableAdapter();
            TA.Fill(PacketHotel, catClaim, null, null, null, null, null, null);
        }

        public void FillTransportPacket(byte[] catClaim)
        {
            SamoDBTableAdapters.PacketFreightTableAdapter TA = new SamoDBTableAdapters.PacketFreightTableAdapter();
            TA.Fill(PacketFreight, catClaim, null, null, null, null);
        }

        public void FillServicePacket(byte[] catClaim)
        {
            SamoDBTableAdapters.PacketServiceTableAdapter TA = new SamoDBTableAdapters.PacketServiceTableAdapter();
            TA.Fill(PacketService, catClaim, null, null, null, null);
        }

        public void FillPacketInfo(byte[] catClaim, int? partner = null)
        {
            SamoDBTableAdapters.PacketInfoTableAdapter TA = new SamoDBTableAdapters.PacketInfoTableAdapter();
            TA.Fill(PacketInfo, catClaim, partner, null, null, null, null, null);
        }

        public void FillCatClaimUnpack(byte[] catClaim)
        {
            SamoDBTableAdapters.uf_web_4_cat_claim_unpackTableAdapter TA = new SamoDBTableAdapters.uf_web_4_cat_claim_unpackTableAdapter();
            TA.Fill(uf_web_4_cat_claim_unpack, catClaim);
        }

        public void BronCalcSave(byte[] catClaim, int? tour, int nights, bool save, bool check, string note, int partpass, int user, string noteclaim, int owner, int internerPartner, string claimDocument, Guid claimGuid)
        {
            SamoDBTableAdapters.bron_calc_saveTableAdapter TA = new SamoDBTableAdapters.bron_calc_saveTableAdapter();
            TA.Fill(bron_calc_save, tour, null, null, null, save, check, note, partpass, user, true, true, noteclaim, owner, false, internerPartner, null, false, catClaim, false, false, nights, null, null, claimDocument, 21, claimGuid.ToString(), null);
            
        }


        internal void FillVisas(byte[] catClaim)
        {
            SamoDBTableAdapters.PacketVisaTableAdapter TA = new SamoDBTableAdapters.PacketVisaTableAdapter();
            TA.Fill(PacketVisa, catClaim, null, null, null, 0, "");
        }

        internal int GetPartPass()
        {
            SamoDBTableAdapters.QueriesTableAdapter TA = new SamoDBTableAdapters.QueriesTableAdapter();
            int partppass = (int)TA.GetPartPass(Properties.Settings.Default.PartPass);
            if (partppass > 0)
            {
                return partppass;
            }
            else
            {
                throw new Exception("Login not found");
            }
            
        }

        internal int GetPartner(int partpass)
        {
            SamoDBTableAdapters.QueriesTableAdapter TA = new SamoDBTableAdapters.QueriesTableAdapter();
            int partner = (int)TA.GetPartnerByPartpass(partpass);
            if (partner > 0)
            {
                return partner;
            }
            else
            {
                throw new Exception("Partner not found");
            }
            
        }

        internal int GetInternetUser()
        {
            SamoDBTableAdapters.QueriesTableAdapter TA = new SamoDBTableAdapters.QueriesTableAdapter();
            var r = TA.GetInternetUserCode(true);
            int result = (int)r;
            return result;
        }

        internal void FillTourConfig(int tour, int usercode)
        {
            SamoDBTableAdapters.up_WEB_3_tour_configTableAdapter TA = new SamoDBTableAdapters.up_WEB_3_tour_configTableAdapter();
            TA.Fill(up_WEB_3_tour_config, tour, null, null, "online_config", null, (short)usercode);
        }
    }
}
