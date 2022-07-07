public static class GameBalanceValues
{
    public static bool isTutorialActive;
    public static int StartedCatAmanont()
    {
        if (isTutorialActive)
            return 6;
        else
            return 5;
    }
    public static int BaseForLevelUpAmount(int seviye)
    {
        int miktar = 0;
        switch (seviye)
        {
            case 1:
                miktar = 50;
                break;
            case 2:
                miktar = 250;
                break;
            case 3:
                miktar = 450;
                break;
            case 4:
                miktar = 900;
                break;
            case 5:
                miktar = 1600;
                break;
            case 6:
                miktar = 3200;
                break;
            case 7:
                miktar = 4800;
                break;
        }
        return miktar;
    }
    
    public static int BaseCapacityIncreaseAmount(MerkezlerBase.MerkezType merkezType, int seviye)
    {
        int miktar = 0;
        switch (merkezType)
        {
            case MerkezlerBase.MerkezType.Castle:
                switch (seviye)
                {
                    case 1:
                        miktar = 2;
                        break;
                    case 2:
                        miktar = 3;
                        break;
                    case 3:
                        miktar = 4;
                        break;
                    case 4:
                        miktar = 8;
                        break;
                    case 5:
                        miktar = 20;
                        break;
                    case 6:
                        miktar = 20;
                        break;
                    case 7:
                        miktar = 20;
                        break;
                }
                break;
            case MerkezlerBase.MerkezType.Rest:
                switch (seviye)
                {
                    case 1:
                        miktar = 5;
                        break;
                    case 2:
                        miktar = 10;
                        break;
                    case 3:
                        miktar = 20;
                        break;
                    case 4:
                        miktar = 30;
                        break;
                    case 5:
                        miktar = 40;
                        break;
                    case 6:
                        miktar = 60;
                        break;
                    case 7:
                        miktar = 80;
                        break;
                }
                break;
            case MerkezlerBase.MerkezType.Mating:
                switch (seviye)
                {
                    case 1:
                        miktar = 2;
                        break;
                    case 2:
                        miktar = 2;
                        break;
                    case 3:
                        miktar = 2;
                        break;
                    case 4:
                        miktar = 2;
                        break;
                    case 5:
                        miktar = 4;
                        break;
                    case 6:
                        miktar = 4;
                        break;
                    case 7:
                        miktar = 4;
                        break;
                }
                break;
            case MerkezlerBase.MerkezType.LightHouse:
                switch (seviye)
                {
                    case 1:
                        miktar = 2;
                        break;
                    case 2:
                        miktar = 3;
                        break;
                    case 3:
                        miktar = 4;
                        break;
                    case 4:
                        miktar = 4;
                        break;
                    case 5:
                        miktar = 8;
                        break;
                    case 6:
                        miktar = 12;
                        break;
                    case 7:
                        miktar = 16;
                        break;
                }
                break;
        }

        return miktar;
    }
    public static int StaminaIncreaseAmount(int seviye)
    {
        int miktar = 0;
        switch (seviye)
        {
            case 1:
                miktar = 30;
                break;
            case 2:
                miktar = 40;
                break;
            case 3:
                miktar = 50;
                break;
            case 4:
                miktar = 60;
                break;
            case 5:
                miktar = 70;
                break;
            case 6:
                miktar = 80;
                break;
            case 7:
                miktar = 110;
                break;
        }
        return miktar;
    }
    public static int KediArtigiArtisMiktari(int seviye)
    {
        /*
        int artis = 0;
        switch (seviye)
        {
            case 1:
                artis = 5;
                break;
            case 2:
                artis = 5;
                break;
            case 3:
                artis = 5;
                break;
            case 4:
                artis = 5;
                break;
            case 5:
                artis = 10;
                break;
            case 6:
                artis = 10;
                break;
            case 7:
                artis = 10;
                break;
        }
        */
        return 5;
    }
    public static int KediArtigiYiyecekArtisMiktari(int seviye)
    {
        int artis = 0;
        switch (seviye)
        {
            case 1:
                artis = 1;
                break;
            case 2:
                artis = 1;
                break;
            case 3:
                artis = 1;
                break;
        }
        return artis;
    }
    public static float MezarlikYokEtmeSuresi()
    {
        return 3f;
    }
    public static void SatoPuanKapasitesiniGuncelle()
    {
        switch (CycleManager.Instance.DayTime)
        {
            case 1:
                BuildManager.Instance.SatoPuaniKapasitesi = 150;
                break;
            case 2:
                BuildManager.Instance.SatoPuaniKapasitesi = 280;
                break;
            case 3:
                BuildManager.Instance.SatoPuaniKapasitesi = 400;
                break;
            case 4:
                BuildManager.Instance.SatoPuaniKapasitesi = 580;
                break;
            case 5:
                BuildManager.Instance.SatoPuaniKapasitesi = 700;
                break;
            case 6:
                BuildManager.Instance.SatoPuaniKapasitesi = 850;
                break;
            case 7:
                BuildManager.Instance.SatoPuaniKapasitesi = 1000;
                break;
            case 8:
                BuildManager.Instance.SatoPuaniKapasitesi = 1200;
                break;
            case 9:
                BuildManager.Instance.SatoPuaniKapasitesi = 1500;
                break;
            case 10:
                BuildManager.Instance.SatoPuaniKapasitesi = 1800;
                break;
            case 11:
                BuildManager.Instance.SatoPuaniKapasitesi = 2000;
                break;
            case 12:
                BuildManager.Instance.SatoPuaniKapasitesi = 2800;
                break;
            case 13:
                BuildManager.Instance.SatoPuaniKapasitesi = 3000;
                break;
            case 14:
                BuildManager.Instance.SatoPuaniKapasitesi = 3400;
                break;
            case 15:
                BuildManager.Instance.SatoPuaniKapasitesi = 3800;
                break;
            case 16:
                BuildManager.Instance.SatoPuaniKapasitesi = 4200;
                break;
            case 17:
                BuildManager.Instance.SatoPuaniKapasitesi = 4600;
                break;
            case 18:
                BuildManager.Instance.SatoPuaniKapasitesi = 5000;
                break;
            case 19:
                BuildManager.Instance.SatoPuaniKapasitesi = 5600;
                break;
            case 20:
                BuildManager.Instance.SatoPuaniKapasitesi = 6000;
                break;
            case 21:
                BuildManager.Instance.SatoPuaniKapasitesi = 6400;
                break;
            case 22:
                BuildManager.Instance.SatoPuaniKapasitesi = 6800;
                break;
            case 23:
                BuildManager.Instance.SatoPuaniKapasitesi = 7200;
                break;
            case 24:
                BuildManager.Instance.SatoPuaniKapasitesi = 7400;
                break;
            case 25:
                BuildManager.Instance.SatoPuaniKapasitesi = 8000;
                break;
        }
    }
    public static int YiyecekKapasitesiArttirmaMiktari()
    {
        int miktar = 0;
        switch (LightHouse.Instance.MerkezSeviyesi)
        {
            case 1:
                miktar = 10;
                break;
            case 2:
                miktar = 30;
                break;
            case 3:
                miktar = 60;
                break;
            case 4:
                miktar = 90;
                break;
            case 5:
                miktar = 120;
                break;
            case 6:
                miktar = 180;
                break;
            case 7:
                miktar = 240;
                break;
        }
        return miktar;
    }


    public static int SatoPuaniEksiltmeMiktari(int day)
    {
        int _index = 0;
        switch (day)
        {
            case 1:
                _index = 25;
                break;
            case 2:
                _index = 60;
                break;
            case 3:
                _index = 120;
                break;
            case 4:
                _index = 150;
                break;
            case 5:
                _index = 185;
                break;
            case 6:
                _index = 220;
                break;
            case 7:
                _index = 270;
                break;
            case 8:
                _index = 320;
                break;
            case 9:
                _index = 380;
                break;
            case 10:
                _index = 430;
                break;
            case 11:
                _index = 580;
                break;
            case 12:
                _index = 630;
                break;
            case 13:
                _index = 680;
                break;
            case 14:
                _index = 720;
                break;
            case 15:
                _index = 770;
                break;
            case 16:
                _index = 810;
                break;
            case 17:
                _index = 860;
                break;
            case 18:
                _index = 1000;
                break;
            case 19:
                _index = 1050;
                break;
            case 20:
                _index = 1090;
                break;
            case 21:
                _index = 1130;
                break;
            case 22:
                _index = 1180;
                break;
            case 23:
                _index = 1220;
                break;
            case 24:
                _index = 1270;
                break;
            case 25:
                _index = 1300;
                break;
        }
        return _index;
    }

}
