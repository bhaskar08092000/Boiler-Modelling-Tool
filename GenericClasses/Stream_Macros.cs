using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Object;

/// <summary>
/// Summary description for Stream_Macros  
/// ssss
/// </summary>
public class Stream_Macros
{
    public Stream_Macros()
    {
        //
        // TODO: Add constructor logic here
        //


    }
   
    
          //Function for h_pT
   public double h_pT(double p, double T)
        {
            double h_pT;
            p = toSIunit_p(p);
            T = toSIunit_T(T);
            switch (region_pT(p, T))
            {
                case 1:
                    {
                        return h_pT = fromSIunit_h(h1_pT(p, T));
                        break;
                    }

                case 2:
                    {
                        return h_pT = fromSIunit_h(h2_pT(p, T));
                        break;
                    }

                case 3:
                    {
                        return h_pT = fromSIunit_h(h3_pT(p, T));
                        break;
                    }

                case 4:
                    {
                        return h_pT = 0.0; // CVErr(xlErrValue);
                        
                        break;
                    }

                case 5:
                    {
                        return h_pT = fromSIunit_h(h5_pT(p, T));
                        break;
                    }

                default:
                    {
                        return h_pT= 0.0;   //h_pT = CVErr(xlErrValue);
                        
                        break;
                    }
            }
        }

    public double toSIunit_p(double Ins)
    {
        double toSIunit_p;
        // Translate bar to MPa
        return toSIunit_p = Ins / 10;
    }
    public double toSIunit_T(double Ins)
    {
        double toSIunit_T;
        // Translate degC to Kelvon
        return toSIunit_T = Ins + 273.15;
    }

    public int region_pT(double p, double T)
    {
        int region_pT;
        double ps;

        if (T > 1073.15 & p < 10 & T < 2273.15 & p > 0.000611)
            return region_pT = 5;

        else if (T <= 1073.15 & T > 273.15 & p <= 100 & p > 0.000611)
        {
            if (T > 623.15)
            {
                if (p > B23p_T(T))
                {
                    region_pT = 3;
                    if (T < 647.096)
                    {
                        ps = p4_T(T);
                        if (Math.Abs(p - ps) < 0.00001)
                            return region_pT = 4;
                    }
                }
                else
                    return region_pT = 2;
            }
            else
            {
                ps = p4_T(T);
                if (Math.Abs(p - ps) < 0.00001)
                    return region_pT = 4;
                else if (p > ps)
                    return region_pT = 1;
                else
                    return region_pT = 2;
            }

            return region_pT;
        }
        else
            return region_pT = 0;// **Error, Outside valid area
    }


    public double fromSIunit_h(double Ins)
    {
        double fromSIunit_h = Ins;
        return fromSIunit_h;
    }



    public double p4_T(double T)
    {
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
        // Section 8.1 The Saturation-Pressure Equation
        // Eq 30, Page 33
        double teta, a, b, c, p4_T;
        teta = T - 0.23855557567849 / (T - 650.17534844798);
        a = Math.Pow(teta, 2) + 1167.0521452767 * teta - 724213.16703206;
        b = -17.073846940092 * Math.Pow(teta, 2) + 12020.82470247 * teta - 3232555.0322333;
        c = 14.91510861353 * Math.Pow(teta, 2) - 4823.2657361591 * teta + 405113.40542057;
        return p4_T = Math.Pow((2 * c / (-b + Math.Pow((Math.Pow(b, 2) - 4 * a * c), 0.5))), 4);
    }


    public double B23p_T(double T)
    {
        double B23p_T;
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam
        // 1997
        // Section 4 Auxiliary Equation for the Boundary between Regions 2 and 3
        // Eq 5, Page 5
        return B23p_T = 348.05185628969 - 1.1671859879975 * T + 1.0192970039326E-03 * Math.Pow(T, 2);
    }


    

    public double h2_pT(double p, double T)
    {
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
        // 6 Equations for Region 2, Section. 6.1 Basic Equation
        // Table 11 and 12, Page 14 and 15
        int i;
        double tau, g0_tau, gr_tau;
        double h2_pT;

        const double R = 0.461526; // kJ/(kg K)
        double[] J0 = { 0, 1, -5, -4, -3, -2, -1, 2, 3 };
        double[] n0 = { -9.6927686500217, 10.086655968018, -0.005608791128302, 0.071452738081455, -0.40710498223928, 1.4240819171444, -4.383951131945, -0.28408632460772, 0.021268463753307 };
        double[] Ir = { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5, 6, 6, 6, 7, 7, 7, 8, 8, 9, 10, 10, 10, 16, 16, 18, 20, 20, 20, 21, 22, 23, 24, 24, 24 };
        double[] Jr = { 0, 1, 2, 3, 6, 1, 2, 4, 7, 36, 0, 1, 3, 6, 35, 1, 2, 3, 7, 3, 16, 35, 0, 11, 25, 8, 36, 13, 4, 10, 14, 29, 50, 57, 20, 35, 48, 21, 53, 39, 26, 40, 58 };
        double[] nr = { -1.7731742473213E-03, -0.017834862292358, -0.045996013696365, -0.057581259083432, -0.05032527872793, -3.3032641670203E-05, -1.8948987516315E-04, -3.9392777243355E-03, -0.043797295650573, -2.6674547914087E-05, 2.0481737692309E-08, 4.3870667284435E-07, -3.227767723857E-05, -1.5033924542148E-03, -0.040668253562649, -7.8847309559367E-10, 1.2790717852285E-08, 4.8225372718507E-07, 2.2922076337661E-06, -1.6714766451061E-11, -2.1171472321355E-03, -23.895741934104, -5.905956432427E-18, -1.2621808899101E-06, -0.038946842435739, 1.1256211360459E-11, -8.2311340897998, 1.9809712802088E-08, 1.0406965210174E-19, -1.0234747095929E-13, -1.0018179379511E-09, -8.0882908646985E-11, 0.10693031879409, -0.33662250574171, 8.9185845355421E-25, 3.0629316876232E-13, -4.2002467698208E-06, -5.9056029685639E-26, 3.7826947613457E-06, -1.2768608934681E-15, 7.3087610595061E-29, 5.5414715350778E-17, -9.436970724121E-07 };
        tau = 540 / T;
        g0_tau = 0;
        for (i = 0; i <= 8; i++)
            g0_tau = g0_tau + n0[i] * J0[i] * Math.Pow(tau, (J0[i] - 1));
        gr_tau = 0;
        for (i = 0; i <= 42; i++)
            gr_tau = gr_tau + nr[i] * Math.Pow(p, Ir[i]) * Jr[i] * Math.Pow((tau - 0.5), (Jr[i] - 1));
        return h2_pT = R * T * tau * (g0_tau + gr_tau);
    }


    public double h3_pT(double p, double T)
    {

        double Ts, Low_Bound, High_Bound, h3_pT;
        double hs = 0.0;
        // ver2.6 Start corrected bug
        if (p < 22.06395)
        {
            Ts = T4_p(p);    // Saturation temperature
            if (T <= Ts)
            {
                High_Bound = h4L_p(p); // Max h är liauid h.
                Low_Bound = h1_pT(p, 623.15);
            }
            else
            {
                Low_Bound = h4V_p(p);  // Min h är Vapour h.
                High_Bound = h2_pT(p, B23T_p(p));
            }
        }
        else
        {
            Low_Bound = h1_pT(p, 623.15);
            High_Bound = h2_pT(p, B23T_p(p));
        }
        // ver2.6 End corrected bug
        Ts = T + 1;
        while (Math.Abs(T - Ts) > 0.000001)
        {
            hs = (Low_Bound + High_Bound) / 2;
            Ts = T3_ph(p, hs);
            if (Ts > T)
                High_Bound = hs;
            else
                Low_Bound = hs;
        }
        return h3_pT = hs;
    }


   

    public double B23T_p(double p)
    {
        double B23T_p;
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam
        // 1997
        // Section 4 Auxiliary Equation for the Boundary between Regions 2 and 3
        // Eq 6, Page 6
        return B23T_p = 572.54459862746 + Math.Pow(((p - 13.91883977887) / 1.0192970039326E-03), 0.5);
    }


    public double h5_pT(double p, double T)
    {

        double tau, gamma0_tau, gammar_tau, h5_pT;
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
        // Basic Equation for Region 5
        // Eq 32,33, Page 36, Tables 37-41
        int i;
        const double R = 0.461526;   // kJ/(kg K)
        double[] Ji0 = { 0, 1, -3, -2, -1, 2 };
        double[] ni0 = { -13.179983674201, 6.8540841634434, -0.024805148933466, 0.36901534980333, -3.1161318213925, -0.32961626538917 };
        double[] Iir = { 1, 1, 1, 2, 3 };
        double[] Jir = { 0, 1, 3, 9, 3 };
        double[] nir = { -1.2563183589592E-04, 2.1774678714571E-03, -0.004594282089991, -3.9724828359569E-06, 1.2919228289784E-07 };
        tau = 1000 / T;
        gamma0_tau = 0;
        for (i = 0; i <= 5; i++)
            gamma0_tau = gamma0_tau + ni0[i] * Ji0[i] * Math.Pow(tau, (Ji0[i] - 1));
        gammar_tau = 0;
        for (i = 0; i <= 4; i++)
            gammar_tau = gammar_tau + nir[i] * Math.Pow(p, Iir[i]) * Jir[i] * Math.Pow(tau, (Jir[i] - 1));
        return h5_pT = R * T * tau * (gamma0_tau + gammar_tau);
    }

   

    public double h4L_p(double p)
    {
        double Low_Bound, High_Bound, Ts, h4L_p;
        double ps = 0.0;
        double hs = 0.0;
        if (p > 0.000611657 & p < 22.06395)
        {
            Ts = T4_p(p);
            if (p < 16.529)
                return h4L_p = h1_pT(p, Ts);
            else
            {
                // Iterate to find the the backward solution of p3sat_h
                Low_Bound = 1670.858218;
                High_Bound = 2087.23500164864;
                while (Math.Abs(p - ps) > 0.00001)
                {
                    hs = (Low_Bound + High_Bound) / 2;
                    ps = p3sat_h(hs);
                    if (ps > p)
                        High_Bound = hs;
                    else
                        Low_Bound = hs;
                }

                return h4L_p = hs;
            }
        }
        else
            return h4L_p = 0.0;
    }
    public double h4V_p(double p)
    {
        double Low_Bound, High_Bound, hs, Ts, h4V_p;
        double ps = 0.0;
        if (p > 0.000611657 & p < 22.06395)
        {
            Ts = T4_p(p);
            if (p < 16.529)
                return h4V_p = h2_pT(p, Ts);
            else
            {
                // Iterate to find the the backward solution of p3sat_h
                Low_Bound = 2087.23500164864;
                High_Bound = 2563.592004 + 5; // 5 added to extrapolate to ensure even the border ==350°C solved.
                while (Math.Abs(p - ps) > 0.000001)
                {
                    hs = (Low_Bound + High_Bound) / 2;
                    ps = p3sat_h(hs);
                    if (ps < p)
                        High_Bound = hs;
                    else
                        Low_Bound = hs;
                }
                return h4V_p = (Low_Bound + High_Bound) / 2;    // hs;
            }
        }
        else
            return h4V_p = 0.0; //CVErr(xlErrValue);
    }
    public double p3sat_h(double h)
    {

        double ps, p3sat_h;
        // Revised Supplementary Release on Backward Equations for the Functions T(p,h), v(p,h) and T(p,s), v(p,s) for Region 3 of the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam
        // 2004
        // Section 4 Boundary Equations psat(h) and psat(s) for the Saturation Lines of Region 3
        // Se pictures Page 17, Eq 10, Table 17, Page 18
        int i;
        double[] Ii = { 0, 1, 1, 1, 1, 5, 7, 8, 14, 20, 22, 24, 28, 36 };
        double[] Ji = { 0, 1, 3, 4, 36, 3, 0, 24, 16, 16, 3, 18, 8, 24 };
        double[] ni = { 0.600073641753024, -9.36203654849857, 24.6590798594147, -107.014222858224, -91582131580576.8, -8623.32011700662, -23.5837344740032, 2.52304969384128E+17, -3.89718771997719E+18, -3.33775713645296E+22, 35649946963.6328, -1.48547544720641E+26, 3.30611514838798E+18, 8.13641294467829E+37 };
        h = h / 2600;
        ps = 0;
        for (i = 0; i <= 13; i++)
            ps = ps + ni[i] * Math.Pow((h - 1.02), Ii[i]) * Math.Pow((h - 0.608), Ji[i]);
        return p3sat_h = ps * 22;
    }


    //    ////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



    // Function 2

    public double T_ph(double p, double h)
    {
        double T_ph;
        p = toSIunit_p(p);
        h = toSIunit_h(h);
        switch (region_ph(p, h))
        {
            case 1:
                {
                    T_ph = fromSIunit_T(T1_ph(p, h));
                    break;
                }

            case 2:
                {
                    T_ph = fromSIunit_T(T2_ph(p, h));
                    break;
                }

            case 3:
                {
                    T_ph = fromSIunit_T(T3_ph(p, h));
                    break;
                }

            case 4:
                {
                    T_ph = fromSIunit_T(T4_p(p));
                    break;
                }

            case 5:
                {
                    T_ph = fromSIunit_T(T5_ph(p, h));
                    break;
                }

            default:
                {
                    T_ph = 0.0;   //CVErr(xlErrValue);
                    break;
                }
        }

        return T_ph;
    }
    public double fromSIunit_T(double Ins)
    {
        double fromSIunit_T;
        // Translate Kelvin to degC
        return fromSIunit_T = Ins - 273.15;
    }


    public double toSIunit_h(double Ins)
    {
        double toSIunit_h;
        return toSIunit_h = Ins;
    }

    public double T1_ph(double p, double h)
    {
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
        // 5 Equations for Region 1, Section. 5.1 Basic Equation, 5.2.1 The Backward Equation T ( p,h )
        // Eqution 11, Table 6, Page 10
        int i;
        double T, T1_ph;

        double[] I1 = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 4, 5, 6 };
        double[] J1 = { 0, 1, 2, 6, 22, 32, 0, 1, 2, 3, 4, 10, 32, 10, 32, 10, 32, 32, 32, 32 };
        double[] n1 = { -238.72489924521, 404.21188637945, 113.49746881718, -5.8457616048039, -1.528548241314E-04, -1.0866707695377E-06, -13.391744872602, 43.211039183559, -54.010067170506, 30.535892203916, -6.5964749423638, 9.3965400878363E-03, 1.157364750534E-07, -2.5858641282073E-05, -4.0644363084799E-09, 6.6456186191635E-08, 8.0670734103027E-11, -9.3477771213947E-13, 5.8265442020601E-15, -1.5020185953503E-17 };
        h = h / 2500;
        T = 0;
        for (i = 0; i <= 19; i++)
            T = T + n1[i] * Math.Pow(p, I1[i]) * Math.Pow((h + 1), J1[i]);
        return T1_ph = T;
    }
    public double T2_ph(double p, double h)
    {
        
        int sub_reg;
        int i;
        double Ts, hs, T2_ph;
      

        if (p < 4)
            sub_reg = 1;
        else if (p < (905.84278514723 - 0.67955786399241 * h + 1.2809002730136E-04 * Math.Pow(h, 2)))
            sub_reg = 2;
        else
            sub_reg = 3;

        switch (sub_reg)
        {
            case 1:
                {
                    // Subregion A
                    // Table 20, Eq 22, page 22
                    double[] Ji = {0, 1, 2, 3, 7, 20, 0, 1, 2, 3, 7, 9, 11, 18, 44, 0, 2, 7, 36, 38, 40, 42, 44, 24, 44, 12, 32, 44, 32, 36, 42, 34, 44, 28};
                    double[]  Ii = {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 7};
                    double[]  ni = {1089.8952318288, 849.51654495535, -107.81748091826, 33.153654801263, -7.4232016790248, 11.765048724356, 1.844574935579, -4.1792700549624, 6.2478196935812, -17.344563108114, -200.58176862096, 271.96065473796, -455.11318285818, 3091.9688604755, 252266.40357872, -6.1707422868339E-03, -0.31078046629583, 11.670873077107, 128127984.04046, -985549096.23276, 2822454697.3002, -3594897141.0703, 1722734991.3197, -13551.334240775, 12848734.66465, 1.3865724283226, 235988.32556514, -13105236.545054, 7399.9835474766, -551966.9703006, 3715408.5996233, 19127.72923966, -415351.64835634, -62.459855192507};
                    Ts = 0;
                    hs = h / 2000;
                    for (i = 0; i <= 33; i++)
                        Ts = Ts + ni[i] * Math.Pow(p, (Ii[i])) * Math.Pow((hs - 2.1), Ji[i]);
                    T2_ph = Ts;
                    break;
                }

            case 2:
                {
                    // Subregion B
                    // Table 21, Eq 23, page 23
                    double[] Ji = {0, 1, 2, 12, 18, 24, 28, 40, 0, 2, 6, 12, 18, 24, 28, 40, 2, 8, 18, 40, 1, 2, 12, 24, 2, 12, 18, 24, 28, 40, 18, 24, 40, 28, 2, 28, 1, 40};
                    double[] Ii = {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 6, 7, 7, 9, 9};
                    double[] ni = {1489.5041079516, 743.07798314034, -97.708318797837, 2.4742464705674, -0.63281320016026, 1.1385952129658, -0.47811863648625, 8.5208123431544E-03, 0.93747147377932, 3.3593118604916, 3.3809355601454, 0.16844539671904, 0.73875745236695, -0.47128737436186, 0.15020273139707, -0.002176411421975, -0.021810755324761, -0.10829784403677, -0.046333324635812, 7.1280351959551E-05, 1.1032831789999E-04, 1.8955248387902E-04, 3.0891541160537E-03, 1.3555504554949E-03, 2.8640237477456E-07, -1.0779857357512E-05, -7.6462712454814E-05, 1.4052392818316E-05, -3.1083814331434E-05, -1.0302738212103E-06, 2.821728163504E-07, 1.2704902271945E-06, 7.3803353468292E-08, -1.1030139238909E-08, -8.1456365207833E-14, -2.5180545682962E-11, -1.7565233969407E-18, 8.6934156344163E-15};
                    Ts = 0;
                    hs = h / 2000;
                    for (i = 0; i <= 37; i++)
                        Ts = Ts + ni[i] * Math.Pow((p - 2), (Ii[i])) * Math.Pow((hs - 2.6), Ji[i]);
                    T2_ph = Ts;
                    break;
                }

            default:
                {
                    // Subregion C
                    // Table 22, Eq 24, page 24
                    double[] Ji = {0, 4, 0, 2, 0, 2, 0, 1, 0, 2, 0, 1, 4, 8, 4, 0, 1, 4, 10, 12, 16, 20, 22};
                    double[] Ii = {-7, -7, -6, -6, -5, -5, -2, -2, -1, -1, 0, 0, 1, 1, 2, 6, 6, 6, 6, 6, 6, 6, 6};
                    double[] ni = {-3236839855524.2, 7326335090218.1, 358250899454.47, -583401318515.9, -10783068217.47, 20825544563.171, 610747.83564516, 859777.2253558, -25745.72360417, 31081.088422714, 1208.2315865936, 482.19755109255, 3.7966001272486, -10.842984880077, -0.04536417267666, 1.4559115658698E-13, 1.126159740723E-12, -1.7804982240686E-11, 1.2324579690832E-07, -1.1606921130984E-06, 2.7846367088554E-05, -5.9270038474176E-04, 1.2918582991878E-03};
                    Ts = 0;
                    hs = h / 2000;
                    for (i = 0; i <= 22; i++)
                        Ts = Ts + ni[i] * Math.Pow((p + 25), (Ii[i])) * Math.Pow((hs - 1.8), Ji[i]);
                    T2_ph = Ts;
                    break;
                }
        }
        return T2_ph;
    }

    public double T3_ph(double p, double h)
    {
        int i;
    
     
        // Section 3.3 Backward Equations T(p,h) and v(p,h) for Subregions 3a and 3b
        // Boundary equation, Eq 1 Page 5
        double h3ab, ps, hs, Ts, T3_ph;
        const double R = 0.461526;
        const double tc = 647.096;
        const double pc = 22.064;
        const double rhoc = 322;
        h3ab = (2014.64004206875 + 3.74696550136983 * p - 2.19921901054187E-02 * Math.Pow(p, 2) + 8.7513168600995E-05 * Math.Pow(p, 3));
        if (h < h3ab)
        {
            // Subregion 3a
            // Eq 2, Table 3, Page 7
            double[] Ii = {-12, -12, -12, -12, -12, -12, -12, -12, -10, -10, -10, -8, -8, -8, -8, -5, -3, -2, -2, -2, -1, -1, 0, 0, 1, 3, 3, 4, 4, 10, 12};
            double[] Ji = {0, 1, 2, 6, 14, 16, 20, 22, 1, 5, 12, 0, 2, 4, 10, 2, 0, 1, 3, 4, 0, 2, 0, 1, 1, 0, 1, 0, 3, 4, 5};
            double[] ni = {-1.33645667811215E-07, 4.55912656802978E-06, -1.46294640700979E-05, 6.3934131297008E-03, 372.783927268847, -7186.54377460447, 573494.7521034, -2675693.29111439, -3.34066283302614E-05, -2.45479214069597E-02, 47.8087847764996, 7.64664131818904E-06, 1.28350627676972E-03, 1.71219081377331E-02, -8.51007304583213, -1.36513461629781E-02, -3.84460997596657E-06, 3.37423807911655E-03, -0.551624873066791, 0.72920227710747, -9.92522757376041E-03, -0.119308831407288, 0.793929190615421, 0.454270731799386, 0.20999859125991, -6.42109823904738E-03, -0.023515586860454, 2.52233108341612E-03, -7.64885133368119E-03, 1.36176427574291E-02, -1.33027883575669E-02};
            ps = p / 100;
            hs = h / 2300;
            Ts = 0;
            for (i = 0; i <= 30; i++)
                Ts = Ts + ni[i] * Math.Pow((ps + 0.24), Ii[i]) * Math.Pow((hs - 0.615), Ji[i]);
           return  T3_ph = Ts * 760;
        }
        else
        {
            // Subregion 3b
            // Eq 3, Table 4, Page 7,8
            double[] Ii = {-12, -12, -10, -10, -10, -10, -10, -8, -8, -8, -8, -8, -6, -6, -6, -4, -4, -3, -2, -2, -1, -1, -1, -1, -1, -1, 0, 0, 1, 3, 5, 6, 8};
            double[] Ji = {0, 1, 0, 1, 5, 10, 12, 0, 1, 2, 4, 10, 0, 1, 2, 0, 1, 5, 0, 4, 2, 4, 6, 10, 14, 16, 0, 2, 1, 1, 1, 1, 1};
            double[] ni = {3.2325457364492E-05, -1.27575556587181E-04, -4.75851877356068E-04, 1.56183014181602E-03, 0.105724860113781, -85.8514221132534, 724.140095480911, 2.96475810273257E-03, -5.92721983365988E-03, -1.26305422818666E-02, -0.115716196364853, 84.9000969739595, -1.08602260086615E-02, 1.54304475328851E-02, 7.50455441524466E-02, 2.52520973612982E-02, -6.02507901232996E-02, -3.07622221350501, -5.74011959864879E-02, 5.03471360939849, -0.925081888584834, 3.91733882917546, -77.314600713019, 9493.08762098587, -1410437.19679409, 8491662.30819026, 0.861095729446704, 0.32334644281172, 0.873281936020439, -0.436653048526683, 0.286596714529479, -0.131778331276228, 6.76682064330275E-03};
            hs = h / 2800;
            ps = p / 100;
            Ts = 0;
            for (i = 0; i <= 32; i++)
                Ts = Ts + ni[i] * Math.Pow((ps + 0.298), Ii[i]) * Math.Pow((hs - 0.72), Ji[i]);
            return T3_ph = Ts * 860;
        }
    }

    public double T4_p(double p)
    {
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
        // Section 8.2 The Saturation-Temperature Equation
        // Eq 31, Page 34
        double beta, e, f, g, d, T4_p;
        beta = Math.Pow(p, 0.25);
        e = Math.Pow(beta, 2) - 17.073846940092 * beta + 14.91510861353;
        f = 1167.0521452767 * Math.Pow(beta, 2) + 12020.82470247 * beta - 4823.2657361591;
        g = -724213.16703206 * Math.Pow(beta, 2) - 3232555.0322333 * beta + 405113.40542057;
        d = 2 * g / (-f - Math.Pow((Math.Pow(f, 2) - 4 * e * g), 0.5));
        return T4_p = (650.17534844798 + d - Math.Pow((Math.Pow((650.17534844798 + d), 2) - 4 * (-0.23855557567849 + 650.17534844798 * d)), 0.5)) / 2;
    }

    public double T5_ph(double p, double h)
    {
        // Solve with half interval method
        double Low_Bound, High_Bound, T5_ph;
        double hs = 0.0;
        double Ts = 0.0;
        Low_Bound = 1073.15;
        High_Bound = 2273.15;
        while (Math.Abs(h - hs) > 0.00001)
        {
            Ts = (Low_Bound + High_Bound) / 2;
            hs = h5_pT(p, Ts);
            if (hs > h)
                High_Bound = Ts;
            else
                Low_Bound = Ts;
        }
        return T5_ph = Ts;
    }



    // *3.2 Regions as a function of ph
    public int region_ph(double p, double h)
    {
        double hL, hV, h_45, h_5u, Ts;
        int region_ph;
        // Check if outside pressure limits
        if (p < 0.000611657 | p > 100)
        {
            region_ph = 0;
            return region_ph;

        }

        // Check if outside low h.
        if (h < 0.963 * p + 2.2)
        {
            if (h < h1_pT(p, 273.15))
            {
                region_ph = 0;
                return region_ph;
                
            }
        }

        if (p < 16.5292)
        {
            // Check Region 1
            Ts = T4_p(p);
            hL = 109.6635 * Math.Log(p) + 40.3481 * p + 734.58; // Approximate function for hL_p
            if (Math.Abs(h - hL) < 100)
                hL = h1_pT(p, Ts);
            if (h <= hL)
            {
                region_ph = 1;
                return region_ph;
            
            }
            // Check Region 4
            hV = 45.1768 * Math.Log(p) - 20.158 * p + 2804.4; // Approximate function for hV_p
            if (Math.Abs(h - hV) < 50)
                hV = h2_pT(p, Ts);
            if (h < hV)
            {
                region_ph = 4;
                return region_ph;
                
            }
            // Check upper limit of region 2 Quick Test
            if (h < 4000)
            {
                region_ph = 2;
                return region_ph;
               
            }
            // Check region 2 (Real value)
            h_45 = h2_pT(p, 1073.15);
            if (h <= h_45)
            {
                region_ph = 2;
                return region_ph;
              
            }
            // Check region 5
            if (p > 10)
            {
                region_ph = 0;
                return region_ph;
              
            }
            h_5u = h5_pT(p, 2273.15);
            if (h < h_5u)
            {
                region_ph = 5;
                return region_ph;
              
            }
           // region_ph = 0;
            //return region_ph;
        }
        else
        {
            // Check if in region1
            if (h < h1_pT(p, 623.15))
            {
                region_ph = 1;
                return region_ph;
              
            }
            // Check if in region 3 or 4 (Bellow Reg 2)
            if (h < h2_pT(p, B23T_p(p)))
            {
                // Region 3 or 4
                if (p > p3sat_h(h))
                {
                    region_ph = 3;
                    return region_ph;
                   
                }
                else
                {
                    region_ph = 4;
                    return region_ph;
                   
                }
            }
            // Check if region 2
            if (h < h2_pT(p, 1073.15))
            {
                region_ph = 2;
                return region_ph;
            
            }
        }
        region_ph = 0;
       return region_ph;
    }

    public double h1_pT(double p, double T)
    {
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
        // 5 Equations for Region 1, Section. 5.1 Basic Equation
        // Eqution 7, Table 3, Page 6
        int i;
        double ps, tau, g_t, h1_pT;

        const double R = 0.461526; // kJ/(kg K)
        double[] I1 = { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 8, 8, 21, 23, 29, 30, 31, 32 };
        double[] J1 = { -2, -1, 0, 1, 2, 3, 4, 5, -9, -7, -1, 0, 1, 3, -3, 0, 1, 3, 17, -4, 0, 6, -5, -2, 10, -8, -11, -6, -29, -31, -38, -39, -40, -41 };
        double[] n1 = { 0.14632971213167, -0.84548187169114, -3.756360367204, 3.3855169168385, -0.95791963387872, 0.15772038513228, -0.016616417199501, 8.1214629983568E-04, 2.8319080123804E-04, -6.0706301565874E-04, -0.018990068218419, -0.032529748770505, -0.021841717175414, -5.283835796993E-05, -4.7184321073267E-04, -3.0001780793026E-04, 4.7661393906987E-05, -4.4141845330846E-06, -7.2694996297594E-16, -3.1679644845054E-05, -2.8270797985312E-06, -8.5205128120103E-10, -2.2425281908E-06, -6.5171222895601E-07, -1.4341729937924E-13, -4.0516996860117E-07, -1.2734301741641E-09, -1.7424871230634E-10, -6.8762131295531E-19, 1.4478307828521E-20, 2.6335781662795E-23, -1.1947622640071E-23, 1.8228094581404E-24, -9.3537087292458E-26 };
        p = p / 16.53;
        tau = 1386 / T;
        g_t = 0;
        for (i = 0; i <= 33; i++)
            g_t = g_t + (n1[i] * Math.Pow((7.1 - p), I1[i]) * J1[i] * Math.Pow((tau - 1.222), (J1[i] - 1)));
        h1_pT = R * T * tau * g_t;
        return h1_pT;
    }


//Function 3
    public double v_pT(double p, double T)
    {
        double v_pT;
        p = toSIunit_p(p);
        T = toSIunit_T(T);
        switch (region_pT(p, T))
        {
            case 1:
                {
                    return v_pT = fromSIunit_v(v1_pT(p, T));
                    break;
                }

            case 2:
                {
                    return v_pT = fromSIunit_v(v2_pT(p, T));
                    break;
                }

            case 3:
                {
                    return v_pT = fromSIunit_v(v3_ph(p, h3_pT(p, T)));
                    break;
                }

            case 4:
                {
                    return v_pT = 0.0;  //CVErr(xlErrValue);
                    break;
                }

            case 5:
                {
                    return v_pT = fromSIunit_v(v5_pT(p, T));
                    break;
                }

            default:
                {
                    return v_pT = 0.0;          // CVErr(xlErrValue);
                    break;
                }
        }
    }
    public double fromSIunit_v(double Ins)
    {
       double fromSIunit_v = Ins;
       return fromSIunit_v;
    }

    public double v1_pT(double p, double T)
    {
       
        int i;
        double ps, tau, g_p, v1_pT;
    
        const double R = 0.461526; // kJ/(kg K)
        double[] I1 = {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 8, 8, 21, 23, 29, 30, 31, 32};
        double[] J1 = {-2, -1, 0, 1, 2, 3, 4, 5, -9, -7, -1, 0, 1, 3, -3, 0, 1, 3, 17, -4, 0, 6, -5, -2, 10, -8, -11, -6, -29, -31, -38, -39, -40, -41};
        double[] n1 = {0.14632971213167, -0.84548187169114, -3.756360367204, 3.3855169168385, -0.95791963387872, 0.15772038513228, -0.016616417199501, 8.1214629983568E-04, 2.8319080123804E-04, -6.0706301565874E-04, -0.018990068218419, -0.032529748770505, -0.021841717175414, -5.283835796993E-05, -4.7184321073267E-04, -3.0001780793026E-04, 4.7661393906987E-05, -4.4141845330846E-06, -7.2694996297594E-16, -3.1679644845054E-05, -2.8270797985312E-06, -8.5205128120103E-10, -2.2425281908E-06, -6.5171222895601E-07, -1.4341729937924E-13, -4.0516996860117E-07, -1.2734301741641E-09, -1.7424871230634E-10, -6.8762131295531E-19, 1.4478307828521E-20, 2.6335781662795E-23, -1.1947622640071E-23, 1.8228094581404E-24, -9.3537087292458E-26};
        ps = p / 16.53;
        tau = 1386 / T;
        g_p = 0;
        for (i = 0; i <= 33; i++)
            g_p = g_p - n1[i] * I1[i] * Math.Pow((7.1 - ps), (I1[i] - 1)) * Math.Pow((tau - 1.222), J1[i]);
        return v1_pT = R * T / p * ps * g_p / 1000;
    }

    public double v2_pT(double p, double T)
    {
        
        int i;
        double tau, g0_pi, gr_pi, v2_pT;
      
        const double R = 0.461526; // kJ/(kg K)
        double[] J0 = {0, 1, -5, -4, -3, -2, -1, 2, 3};
        double[] n0 = {-9.6927686500217, 10.086655968018, -0.005608791128302, 0.071452738081455, -0.40710498223928, 1.4240819171444, -4.383951131945, -0.28408632460772, 0.021268463753307};
        double[] Ir = {1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5, 6, 6, 6, 7, 7, 7, 8, 8, 9, 10, 10, 10, 16, 16, 18, 20, 20, 20, 21, 22, 23, 24, 24, 24};
        double[] Jr = {0, 1, 2, 3, 6, 1, 2, 4, 7, 36, 0, 1, 3, 6, 35, 1, 2, 3, 7, 3, 16, 35, 0, 11, 25, 8, 36, 13, 4, 10, 14, 29, 50, 57, 20, 35, 48, 21, 53, 39, 26, 40, 58};
        double[] nr = {-1.7731742473213E-03, -0.017834862292358, -0.045996013696365, -0.057581259083432, -0.05032527872793, -3.3032641670203E-05, -1.8948987516315E-04, -3.9392777243355E-03, -0.043797295650573, -2.6674547914087E-05, 2.0481737692309E-08, 4.3870667284435E-07, -3.227767723857E-05, -1.5033924542148E-03, -0.040668253562649, -7.8847309559367E-10, 1.2790717852285E-08, 4.8225372718507E-07, 2.2922076337661E-06, -1.6714766451061E-11, -2.1171472321355E-03, -23.895741934104, -5.905956432427E-18, -1.2621808899101E-06, -0.038946842435739, 1.1256211360459E-11, -8.2311340897998, 1.9809712802088E-08, 1.0406965210174E-19, -1.0234747095929E-13, -1.0018179379511E-09, -8.0882908646985E-11, 0.10693031879409, -0.33662250574171, 8.9185845355421E-25, 3.0629316876232E-13, -4.2002467698208E-06, -5.9056029685639E-26, 3.7826947613457E-06, -1.2768608934681E-15, 7.3087610595061E-29, 5.5414715350778E-17, -9.436970724121E-07};
        tau = 540 / T;
        g0_pi = 1 / p;
        gr_pi = 0;
        for (i = 0; i <= 42; i++)
            gr_pi = gr_pi + nr[i] * Ir[i] * Math.Pow(p, (Ir[i] - 1)) * Math.Pow((tau - 0.5), Jr[i]);
        return v2_pT = R * T / p * p * (g0_pi + gr_pi) / 1000;
    }

    public double v3_ph(double p, double h)
    {
        int i;


        double h3ab, ps, hs, vs, v3_ph;
        const double R = 0.461526;
        const double tc = 647.096;
        const double pc = 22.064;
        const double rhoc = 322;
        h3ab = (2014.64004206875 + 3.74696550136983 * p - 2.19921901054187E-02 * Math.Pow(p, 2) + 8.7513168600995E-05 * Math.Pow(p, 3));
        if (h < h3ab)
        {
            // Subregion 3a
            // Eq 4, Table 6, Page 9
            double[] Ii = {-12, -12, -12, -12, -10, -10, -10, -8, -8, -6, -6, -6, -4, -4, -3, -2, -2, -1, -1, -1, -1, 0, 0, 1, 1, 1, 2, 2, 3, 4, 5, 8};
            double[] Ji = {6, 8, 12, 18, 4, 7, 10, 5, 12, 3, 4, 22, 2, 3, 7, 3, 16, 0, 1, 2, 3, 0, 1, 0, 1, 2, 0, 2, 0, 2, 2, 2};
            double[] ni = {5.29944062966028E-03, -0.170099690234461, 11.1323814312927, -2178.98123145125, -5.06061827980875E-04, 0.556495239685324, -9.43672726094016, -0.297856807561527, 93.9353943717186, 1.92944939465981E-02, 0.421740664704763, -3689141.2628233, -7.37566847600639E-03, -0.354753242424366, -1.99768169338727, 1.15456297059049, 5683.6687581596, 8.08169540124668E-03, 0.172416341519307, 1.04270175292927, -0.297691372792847, 0.560394465163593, 0.275234661176914, -0.148347894866012, -6.51142513478515E-02, -2.92468715386302, 6.64876096952665E-02, 3.52335014263844, -1.46340792313332E-02, -2.24503486668184, 1.10533464706142, -4.08757344495612E-02};
            ps = p / 100;
            hs = h / 2100;
            vs = 0;
            for (i = 0; i <= 31; i++)
                vs = vs + ni[i] * Math.Pow((ps + 0.128), Ii[i]) * Math.Pow((hs - 0.727), Ji[i]);
            return v3_ph = vs * 0.0028;
        }
        else
        {
            // Subregion 3b
            // Eq 5, Table 7, Page 9
            double[] Ii = {-12, -12, -8, -8, -8, -8, -8, -8, -6, -6, -6, -6, -6, -6, -4, -4, -4, -3, -3, -2, -2, -1, -1, -1, -1, 0, 1, 1, 2, 2};
            double[] Ji = {0, 1, 0, 1, 3, 6, 7, 8, 0, 1, 2, 5, 6, 10, 3, 6, 10, 0, 2, 1, 2, 0, 1, 4, 5, 0, 0, 1, 2, 6};
            double[] ni = {-2.25196934336318E-09, 1.40674363313486E-08, 2.3378408528056E-06, -3.31833715229001E-05, 1.07956778514318E-03, -0.271382067378863, 1.07202262490333, -0.853821329075382, -2.15214194340526E-05, 7.6965608822273E-04, -4.31136580433864E-03, 0.453342167309331, -0.507749535873652, -100.475154528389, -0.219201924648793, -3.21087965668917, 607.567815637771, 5.57686450685932E-04, 0.18749904002955, 9.05368030448107E-03, 0.285417173048685, 3.29924030996098E-02, 0.239897419685483, 4.82754995951394, -11.8035753702231, 0.169490044091791, -1.79967222507787E-02, 3.71810116332674E-02, -5.36288335065096E-02, 1.6069710109252};
            ps = p / 100;
            hs = h / 2800;
            vs = 0;
            for (i = 0; i <= 29; i++)
                vs = vs + ni[i] * Math.Pow((ps + 0.0661), Ii[i]) * Math.Pow((hs - 0.72), Ji[i]);
            return v3_ph = vs * 0.0088;
        }
    }

    public double v5_pT(double p, double T)
    {

        double tau, gamma0_pi, gammar_pi, v5_pT;
        // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
        // Basic Equation for Region 5
        // Eq 32,33, Page 36, Tables 37-41
        int i;
        const double R = 0.461526;   // kJ/(kg K)
        double[] Ji0 = {0, 1, -3, -2, -1, 2};
        double[] ni0 = {-13.179983674201, 6.8540841634434, -0.024805148933466, 0.36901534980333, -3.1161318213925, -0.32961626538917};
        double[] Iir = {1, 1, 1, 2, 3};
        double[] Jir = {0, 1, 3, 9, 3};
        double[] nir = {-1.2563183589592E-04, 2.1774678714571E-03, -0.004594282089991, -3.9724828359569E-06, 1.2919228289784E-07};
        tau = 1000 / T;
        gamma0_pi = 1 / p;
        gammar_pi = 0;
        for (i = 0; i <= 4; i++)
            gammar_pi = gammar_pi + nir[i] * Iir[i] * Math.Pow(p, (Iir[i] - 1)) * Math.Pow(tau, Jir[i]);
        return v5_pT = R * T / p * p * (gamma0_pi + gammar_pi) / 1000;
    }


    //Function 4
    public double tc_pT(double p, double T)
    {
        double v, tc_pT;
        v = v_pT(p, T);
        p = toSIunit_p(p);
        T = toSIunit_T(T);
        v = toSIunit_v(v);
        return tc_pT = fromSIunit_tc(tc_ptrho(p, T, 1 / v));
    }

    public double toSIunit_v(double Ins)
    {
        double toSIunit_v = Ins;
        return toSIunit_v;
    }

    public double fromSIunit_tc(double Ins)
    {
        double fromSIunit_tc = Ins;
        return fromSIunit_tc;
    }

    public double tc_ptrho(double p, double T, double rho)
    {

        double tc0, tc1, dT, Q, s, tc2, tc, tc_ptrho;
        if (T < 273.15)
        {
            tc_ptrho = 0.0;      //CVErr(xlErrValue); 
            return tc_ptrho;
        }
        else if (T < 500 + 273.15)
        {
            if (p > 100)
            {
                tc_ptrho = 0.0;     //CVErr(xlErrValue); 
                return tc_ptrho;
            }
        }
        else if (T <= 650 + 273.15)
        {
            if (p > 70)
            {
                tc_ptrho = 0.0;     //      CVErr(xlErrValue); 
                return tc_ptrho;
            }
        }
        else if (T <= 800 + 273.15)
        {
            if (p > 40)
            {
                tc_ptrho = 0.0;         // CVErr(xlErrValue); 
                return tc_ptrho;
            }
        }
        // ver2.6 End corrected bug

        T = T / 647.26;
        rho = rho / 317.7;
        tc0 = Math.Pow(T, 0.5) * (0.0102811 + 0.0299621 * T + 0.0156146 * Math.Pow(T, 2) - 0.00422464 * Math.Pow(T, 3));
        tc1 = -0.39707 + 0.400302 * rho + 1.06 * Math.Exp(-0.171587 * Math.Pow((rho + 2.39219), 2));
        dT = Math.Abs(T - 1) + 0.00308976;
        Q = 2 + 0.0822994 / Math.Pow(dT, (3 / (double)5));
        if (T >= 1)
            s = 1 / dT;
        else
            s = 10.0932 / Math.Pow(dT, (3 / (double)5));
        tc2 = (0.0701309 / Math.Pow(T, 10) + 0.011852) * Math.Pow(rho, (9 / (double)5)) * Math.Exp(0.642857 * (1 - Math.Pow(rho, (14 / (double)5)))) + 0.00169937 * s * Math.Pow(rho, Q) * Math.Exp((Q / (1 + Q)) * (1 - Math.Pow(rho, (1 + Q)))) - 1.02 * Math.Exp(-4.11717 * Math.Pow(T, (3 / (double)2)) - 6.17937 / Math.Pow(rho, 5));

        return tc_ptrho = tc0 + tc1 + tc2;
    }


        //Function 5
        public double my_pT(double p, double T)
        {
            double my_pT;
            p = toSIunit_p(p);
            T = toSIunit_T(T);
            switch (region_pT(p, T))
            {
                case 4:
                    {
                       return my_pT = 0.0;        //CVErr(xlErrValue);
                        break;
                    }

                case 1:
                case 2:
                case 3:
                case 5:
                    {
                        return my_pT = fromSIunit_my(my_AllRegions_pT(p, T));
                        break;
                    }

                default:
                    {
                        return my_pT = 0.0;     //CVErr(xlErrValue);
                        break;
                    }
            }
        }

        public double fromSIunit_my(double Ins)
        {
            double fromSIunit_my = Ins;
            return fromSIunit_my;
        }

        public double my_AllRegions_pT(double p, double T)
        {
           
            double rho, Ts, ps, my0, sum, my1, rhos,my_AllRegions_pT;
            int i;
            double[] h0 = {0.5132047, 0.3205656, 0, 0, -0.7782567, 0.1885447};
            double[] h1 = {0.2151778, 0.7317883, 1.241044, 1.476783, 0, 0};
            double[] h2 = {-0.2818107, -1.070786, -1.263184, 0, 0, 0};
            double[] h3 = {0.1778064, 0.460504, 0.2340379, -0.4924179, 0, 0};
            double[] h4 = {-0.0417661, 0, 0, 0.1600435, 0, 0};
            double[] h5 = {0, -0.01578386, 0, 0, 0, 0};
            double[] h6 = {0, 0, 0, -0.003629481, 0, 0};

            // Calcualte density.
            switch (region_pT(p, T))
            {
                case 1:
                    {
                        rho = 1 / (double)v1_pT(p, T);
                        break;
                    }

                case 2:
                    {
                        rho = 1 / (double)v2_pT(p, T);
                        break;
                    }

                case 3:
                    {
                        rho = 1 / (double)v3_ph(p, h3_pT(p, T));
                        break;
                    }

                case 4:
                    {
                        rho = 0.0;      //      CVErr(xlErrValue);
                        break;
                    }

                case 5:
                    {
                        rho = 1 / (double)v5_pT(p, T);
                        break;
                    }

                default:
                    {
                        my_AllRegions_pT = 0.0;     // CVErr(xlErrValue);
                        return my_AllRegions_pT;
                    }
            }

            rhos = rho / 317.763;
            Ts = T / 647.226;
            ps = p / 22.115;

            // Check valid area
            if (T > 900 + 273.15 | (T > 600 + 273.15 & p > 300) | (T > 150 + 273.15 & p > 350) | p > 500)
            {
                my_AllRegions_pT =  0.0 ;   // CVErr(xlErrValue);
                return my_AllRegions_pT;
            }
            my0 = Math.Pow(Ts, 0.5) / (1 + 0.978197 / Ts + 0.579829 / (Math.Pow(Ts, 2)) - 0.202354 / (Math.Pow(Ts, 3)));
            sum = 0;
            for (i = 0; i <= 5; i++)
                sum = sum + h0[i] * Math.Pow((1 / Ts - 1), i) + h1[i] * Math.Pow((1 / Ts - 1), i) * Math.Pow((rhos - 1), 1) + h2[i] * Math.Pow((1 / Ts - 1), i) * Math.Pow((rhos - 1), 2) + h3[i] * Math.Pow((1 / Ts - 1), i) * Math.Pow((rhos - 1), 3) + h4[i] * Math.Pow((1 / Ts - 1), i) * Math.Pow((rhos - 1), 4) + h5[i] * Math.Pow((1 / Ts - 1), i) * Math.Pow((rhos - 1), 5) + h6[i] * Math.Pow((1 / Ts - 1), i) * Math.Pow((rhos - 1), 6);
            my1 = Math.Exp(rhos * sum);
            return my_AllRegions_pT = my0 * my1 * 0.000055071;
        }



    //Function 6

        public double Pr_pT(double p, double T)
        {
            double Cp;
            double my;
            double tc;
            double Pr_pT;
            Cp = toSIunit_Cp(Cp_pT(p, T));
            my = toSIunit_my(my_pT(p, T));
            tc = toSIunit_tc(tc_pT(p, T));
            return Pr_pT = Cp * 1000 * my / tc;
        }

        public double toSIunit_Cp(double Ins)
        {

            double toSIunit_Cp = Ins;
            return toSIunit_Cp;
        }

        public double toSIunit_my(double Ins)
        {
            double toSIunit_my = Ins;
            return toSIunit_my;
        }

        public double toSIunit_tc(double Ins)
        {
            double toSIunit_tc = Ins;
            return toSIunit_tc;
        }
        public double Cp_pT(double p, double T)
        {
            double Cp_pT;
            p = toSIunit_p(p);
            T = toSIunit_T(T);
            switch (region_pT(p, T))
            {
                case 1:
                    {
                        return Cp_pT = fromSIunit_Cp(Cp1_pT(p, T));
                        break;
                    }

                case 2:
                    {
                        return Cp_pT = fromSIunit_Cp(Cp2_pT(p, T));
                        break;
                    }

                case 3:
                    {
                        return Cp_pT = fromSIunit_Cp(Cp3_rhoT(1 / (double)v3_ph(p, h3_pT(p, T)), T));
                        break;
                    }

                case 4:
                    {
                        return Cp_pT = 0.0;   //  CVErr(xlErrValue);
                        break;
                    }

                case 5:
                    {
                        return Cp_pT = fromSIunit_Cp(Cp5_pT(p, T));
                        break;
                    }

                default:
                    {
                        return Cp_pT = 0.0;        // CVErr(xlErrValue);
                        break;
                    }
            }
        }
        public double fromSIunit_Cp(double Ins)
        {
            double fromSIunit_Cp = Ins;
            return fromSIunit_Cp;
        }


        public double Cp1_pT(double p, double T)
        {
            // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
            // 5 Equations for Region 1, Section. 5.1 Basic Equation
            // Eqution 7, Table 3, Page 6
            int i;
            double G_tt, Cp1_pT;
         
            const double R = 0.461526; // kJ/(kg K)
            double[] I1 = {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 8, 8, 21, 23, 29, 30, 31, 32};
            double[]  J1 = {-2, -1, 0, 1, 2, 3, 4, 5, -9, -7, -1, 0, 1, 3, -3, 0, 1, 3, 17, -4, 0, 6, -5, -2, 10, -8, -11, -6, -29, -31, -38, -39, -40, -41};
            double[]  n1 = {0.14632971213167, -0.84548187169114, -3.756360367204, 3.3855169168385, -0.95791963387872, 0.15772038513228, -0.016616417199501, 8.1214629983568E-04, 2.8319080123804E-04, -6.0706301565874E-04, -0.018990068218419, -0.032529748770505, -0.021841717175414, -5.283835796993E-05, -4.7184321073267E-04, -3.0001780793026E-04, 4.7661393906987E-05, -4.4141845330846E-06, -7.2694996297594E-16, -3.1679644845054E-05, -2.8270797985312E-06, -8.5205128120103E-10, -2.2425281908E-06, -6.5171222895601E-07, -1.4341729937924E-13, -4.0516996860117E-07, -1.2734301741641E-09, -1.7424871230634E-10, -6.8762131295531E-19, 1.4478307828521E-20, 2.6335781662795E-23, -1.1947622640071E-23, 1.8228094581404E-24, -9.3537087292458E-26};
            p = p / 16.53;
            T = 1386 / T;
            G_tt = 0;
            for (i = 0; i <= 33; i++)
                G_tt = G_tt + (n1[i] * Math.Pow((7.1 - p), I1[i]) * J1[i] * (J1[i] - 1) * Math.Pow((T - 1.222), (J1[i] - 2)));
            return Cp1_pT = -R * Math.Pow(T, 2) * G_tt;
        }

        public double Cp2_pT(double p, double T)
        {
            // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
            // 6 Equations for Region 2, Section. 6.1 Basic Equation
            // Table 11 and 12, Page 14 and 15
            int i;
            double tau, g0_tautau, gr_tautau,Cp2_pT;
            
            const double R = 0.461526; // kJ/(kg K)
            double[] J0 = {0, 1, -5, -4, -3, -2, -1, 2, 3};
            double[] n0 = {-9.6927686500217, 10.086655968018, -0.005608791128302, 0.071452738081455, -0.40710498223928, 1.4240819171444, -4.383951131945, -0.28408632460772, 0.021268463753307};
            double[] Ir = {1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5, 6, 6, 6, 7, 7, 7, 8, 8, 9, 10, 10, 10, 16, 16, 18, 20, 20, 20, 21, 22, 23, 24, 24, 24};
            double[] Jr = {0, 1, 2, 3, 6, 1, 2, 4, 7, 36, 0, 1, 3, 6, 35, 1, 2, 3, 7, 3, 16, 35, 0, 11, 25, 8, 36, 13, 4, 10, 14, 29, 50, 57, 20, 35, 48, 21, 53, 39, 26, 40, 58};
            double[] nr = {-1.7731742473213E-03, -0.017834862292358, -0.045996013696365, -0.057581259083432, -0.05032527872793, -3.3032641670203E-05, -1.8948987516315E-04, -3.9392777243355E-03, -0.043797295650573, -2.6674547914087E-05, 2.0481737692309E-08, 4.3870667284435E-07, -3.227767723857E-05, -1.5033924542148E-03, -0.040668253562649, -7.8847309559367E-10, 1.2790717852285E-08, 4.8225372718507E-07, 2.2922076337661E-06, -1.6714766451061E-11, -2.1171472321355E-03, -23.895741934104, -5.905956432427E-18, -1.2621808899101E-06, -0.038946842435739, 1.1256211360459E-11, -8.2311340897998, 1.9809712802088E-08, 1.0406965210174E-19, -1.0234747095929E-13, -1.0018179379511E-09, -8.0882908646985E-11, 0.10693031879409, -0.33662250574171, 8.9185845355421E-25, 3.0629316876232E-13, -4.2002467698208E-06, -5.9056029685639E-26, 3.7826947613457E-06, -1.2768608934681E-15, 7.3087610595061E-29, 5.5414715350778E-17, -9.436970724121E-07};
            tau = 540 / T;
            g0_tautau = 0;
            for (i = 0; i <= 8; i++)
                g0_tautau = g0_tautau + n0[i] * J0[i] * (J0[i] - 1) * Math.Pow(tau, (J0[i] - 2));
            gr_tautau = 0;
            for (i = 0; i <= 42; i++)
                gr_tautau = gr_tautau + nr[i] * Math.Pow(p, Ir[i]) * Jr[i] * (Jr[i] - 1) * Math.Pow((tau - 0.5), (Jr[i] - 2));
            return Cp2_pT = -R * Math.Pow(tau, 2) * (g0_tautau + gr_tautau);
        }

        public double Cp3_rhoT(double rho, double T)
        {
            int i;
        
            // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
            // 7 Basic Equation for Region 3, Section. 6.1 Basic Equation
            // Table 30 and 31, Page 30 and 31
            double fideltatau, fi, delta, tau, fitautau, fidelta, fideltatautau, fideltadelta,Cp3_rhoT;
            const double R = 0.461526;
            const double tc = 647.096;
            const double pc = 22.064;
            const double rhoc = 322;
            double[] Ii = {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 8, 9, 9, 10, 10, 11};
            double[] Ji = {0, 0, 1, 2, 7, 10, 12, 23, 2, 6, 15, 17, 0, 2, 6, 7, 22, 26, 0, 2, 4, 16, 26, 0, 2, 4, 26, 1, 3, 26, 0, 2, 26, 2, 26, 2, 26, 0, 1, 26};
            double[] ni = {1.0658070028513, -15.732845290239, 20.944396974307, -7.6867707878716, 2.6185947787954, -2.808078114862, 1.2053369696517, -8.4566812812502E-03, -1.2654315477714, -1.1524407806681, 0.88521043984318, -0.64207765181607, 0.38493460186671, -0.85214708824206, 4.8972281541877, -3.0502617256965, 0.039420536879154, 0.12558408424308, -0.2799932969871, 1.389979956946, -2.018991502357, -8.2147637173963E-03, -0.47596035734923, 0.0439840744735, -0.44476435428739, 0.90572070719733, 0.70522450087967, 0.10770512626332, -0.32913623258954, -0.50871062041158, -0.022175400873096, 0.094260751665092, 0.16436278447961, -0.013503372241348, -0.014834345352472, 5.7922953628084E-04, 3.2308904703711E-03, 8.0964802996215E-05, -1.6557679795037E-04, -4.4923899061815E-05};
            delta = rho / rhoc;
            tau = tc / T;
            fitautau = 0;
            fidelta = 0;
            fideltatau = 0;
            fideltadelta = 0;
            for (i = 1; i <= 39; i++)
            {
                fitautau = fitautau + ni[i] * Math.Pow(delta, Ii[i]) * Ji[i] * (Ji[i] - 1) * Math.Pow(tau, (Ji[i] - 2));
                fidelta = fidelta + ni[i] * Ii[i] * Math.Pow(delta, (Ii[i] - 1)) * Math.Pow(tau, Ji[i]);
                fideltatau = fideltatau + ni[i] * Ii[i] * Math.Pow(delta, (Ii[i] - 1)) * Ji[i] * Math.Pow(tau, (Ji[i] - 1));
                fideltadelta = fideltadelta + ni[i] * Ii[i] * (Ii[i] - 1) * Math.Pow(delta, (Ii[i] - 2)) * Math.Pow(tau, Ji[i]);
            }
            fidelta = fidelta + ni[0] / delta;
            fideltadelta = fideltadelta - ni[0] / (Math.Pow(delta, 2));
            return Cp3_rhoT = R * (-(Math.Pow(tau, 2) * fitautau) + Math.Pow((delta * fidelta - delta * tau * fideltatau), 2) / (2 * delta * fidelta + Math.Pow(delta, 2) * fideltadelta));
        }

        public double Cp5_pT(double p, double T)
        {
         
            double tau, gamma0_tautau, gammar_tautau,Cp5_pT;
            // Release on the IAPWS Industrial Formulation 1997 for the Thermodynamic Properties of Water and Steam, September 1997
            // Basic Equation for Region 5
            // Eq 32,33, Page 36, Tables 37-41
            int i;
            const double R = 0.461526;   // kJ/(kg K)
            double[] Ji0 = {0, 1, -3, -2, -1, 2};
            double[] ni0 = {-13.179983674201, 6.8540841634434, -0.024805148933466, 0.36901534980333, -3.1161318213925, -0.3296162653891};
            double[] Iir = {1, 1, 1, 2, 3};
            double[] Jir = {0, 1, 3, 9, 3};
            double[] nir = {-1.2563183589592E-04, 2.1774678714571E-03, -0.004594282089991, -3.9724828359569E-06, 1.2919228289784E-07};
            tau = 1000 / T;
            gamma0_tautau = 0;
            for (i = 0; i <= 5; i++)
                gamma0_tautau = gamma0_tautau + ni0[i] * Ji0[i] * (Ji0[i] - 1) * Math.Pow(tau, (Ji0[i] - 2));
            gammar_tautau = 0;
            for (i = 0; i <= 4; i++)
                gammar_tautau = gammar_tautau + nir[i] * Math.Pow(p, Iir[i]) * Jir[i] * (Jir[i] - 1) * Math.Pow(tau, (Jir[i] - 2));
            return Cp5_pT = -R * Math.Pow(tau, 2) * (gamma0_tautau + gammar_tautau);
        }




    //Function 7
        public double Tsat_p(double p)
        {
            double Tsat_p;
            p = toSIunit_p(p);
            if (p >= 0.000611657 & p <= 22.06395 + 0.001)
                return Tsat_p = fromSIunit_T(T4_p(p));
            else
                return Tsat_p = 0.0;        // CVErr(xlErrValue);
        }


    }


