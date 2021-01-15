        
public void Pogadjaj_adresu()
        {
            this.nadjen_mj_id = 0;
            this.nadjen_ul_id = 0;
            this.nadjen_kbr = 0;
            this.nadjen_alfa = "";
            this.zadan_mjnaz = this.kajmjnaz;
            this.zadan_pttbr = this.kajpttbr;
            this.komanda.CommandText = "select aja_id from adm_mjesta_daisy amd where naziv = '" + this.zadan_mjnaz + "' and id_dostavne_poste = '" + this.zadan_pttbr + "' ";
            this.rrr = this.komanda.ExecuteReader();
            if (!this.rrr.HasRows)
            {
                this.rrr.Close();
            }
            else
            {
                this.pom1 = 0;
                while (this.rrr.Read())
                {
                    checked { ++this.pom1; }
                    if (this.pom1 <= 1)
                        this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                    else
                        break;
                }
                this.rrr.Close();
                if (this.pom1 <= 1)
                {
                    this.nadjen_mj_id = this.pom2;
                    goto label_18;
                }
            }
            this.komanda.CommandText = "select adm_mj_id from mob_upar_sifre  mobus where mobus.mjtxt = '" + this.zadan_mjnaz + "' and id_dostavne_poste = '" + this.zadan_pttbr + "' and mobus.ultxt is null ";
            this.rrr = this.komanda.ExecuteReader();
            if (!this.rrr.HasRows)
            {
                this.rrr.Close();
            }
            else
            {
                this.pom1 = 0;
                while (this.rrr.Read())
                {
                    checked { ++this.pom1; }
                    if (this.pom1 <= 1)
                        this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                    else
                        break;
                }
                this.rrr.Close();
                if (this.pom1 <= 1 && this.pom2 != 0)
                {
                    this.nadjen_mj_id = this.pom2;
                    goto label_18;
                }
            }
            this.nadjen_mj_id = checked(4999900 + this.ptt_tkc(this.zadan_pttbr.PadRight(2).Substring(0, 2)));
            this.nadjen_ul_id = checked(this.nadjen_mj_id + 5000000);
            this.komanda.CommandText = "select count(*) from mob_upar_sifre where mjtxt = '" + this.zadan_mjnaz + "'and id_dostavne_poste = '" + this.zadan_pttbr + "' ";
            this.pom4 = Conversions.ToInteger(this.komanda.ExecuteScalar());
            if (this.pom4 <= 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.zadan_mjnaz.Trim(), "", false) != 0)
            {
                this.pom4 = this.pom4;
                this.komanda.CommandText = "insert into mob_upar_sifre (mjtxt, id_dostavne_poste, adm_mj_id, ultxt, adm_ul_id, datum_inserta, imeop, PRIMJER_tel) values ('" + this.zadan_mjnaz + "', '" + this.zadan_pttbr + "', 0, '', 0, sysdate, '" + this.tekuci_op_naziv + "', '" + this.kajtel + "')";
                this.komanda.ExecuteNonQuery();
                goto label_116;
            }
            else
                goto label_116;
            label_18:
            if (this.kajulica.Length == 0)
            {
                this.nadjen_ul_id = checked(this.nadjen_mj_id + 5000000);
                this.nadjen_kbr = 0;
                this.nadjen_alfa = "BB";
            }
            else
            {
                this.nadjen_alfa = this.longstr.Substring(checked(this.tabic[6] + 1), checked(this.tabic[7] - this.tabic[6] - 1)).ToUpper();
                this.kajkbr = this.longstr.Substring(checked(this.tabic[5] + 1), checked(this.tabic[6] - this.tabic[5] - 1)).Trim(' ').ToUpper();
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.nadjen_alfa, "", false) == 0)
                {
                    this.pomkbr = "";
                    this.pom8 = this.kajkbr.Length;
                    int num = checked(this.pom8 - 1);
                    this.pom7 = 0;
                    while (this.pom7 <= num && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.kajkbr.Substring(0, 1), "0", false) >= 0 & Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.kajkbr.Substring(0, 1), "9", false) <= 0)
                    {
                        this.pomkbr += this.kajkbr.Substring(0, 1);
                        this.kajkbr = this.kajkbr.Substring(1);
                        checked { ++this.pom7; }
                    }
                    this.nadjen_alfa = this.kajkbr;
                    this.kajkbr = this.pomkbr;
                }
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.kajkbr, "", false) == 0 | Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.kajkbr, "0", false) == 0)
                {
                    this.nadjen_kbr = 0;
                    this.nadjen_alfa = "BB";
                }
                else
                {
                    if (Strings.InStr(this.kajkbr, "/") > 0)
                    {
                        this.pom7 = Strings.InStr(this.kajkbr, "/");
                        this.nadjen_alfa += this.kajkbr.Substring(checked(this.pom7 - 1));
                        this.kajkbr = this.kajkbr.Substring(0, checked(this.pom7 - 1));
                    }
                    if (!Versioned.IsNumeric((object)this.kajkbr))
                    {
                        this.nadjen_kbr = 0;
                        this.nadjen_alfa = "BB";
                        this.Dopisi(this.kojidat() + " - Kućni broj nije numerika - " + this.kajtel);
                    }
                    else
                        this.nadjen_kbr = Conversions.ToInteger(this.kajkbr);
                }
                this.komanda.CommandText = "select id from adm_ulice_daisy aud where naziv = '" + this.apo_kajulica + "' and aja_aja_id = " + Conversions.ToString(this.nadjen_mj_id);
                this.rrr = this.komanda.ExecuteReader();
                if (!this.rrr.HasRows)
                {
                    this.rrr.Close();
                }
                else
                {
                    this.pom1 = 0;
                    while (this.rrr.Read())
                    {
                        checked { ++this.pom1; }
                        if (this.pom1 <= 1)
                            this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                        else
                            break;
                    }
                    this.rrr.Close();
                    if (this.pom1 <= 1)
                    {
                        this.nadjen_ul_id = this.pom2;
                        goto label_116;
                    }
                }
                if (Strings.InStr(this.apo_kajulica, "ULICA") > 0)
                    this.pom1 = this.pom1;
                this.komanda.CommandText = "select id from adm_ulice_daisy aud where naziv = TRIM(REPLACE(replace('" + this.apo_kajulica + "',  'ULICA',''),'  ',' ')) and aja_aja_id = " + Conversions.ToString(this.nadjen_mj_id);
                this.rrr = this.komanda.ExecuteReader();
                if (!this.rrr.HasRows)
                {
                    this.rrr.Close();
                }
                else
                {
                    this.pom1 = 0;
                    while (this.rrr.Read())
                    {
                        checked { ++this.pom1; }
                        if (this.pom1 <= 1)
                            this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                        else
                            break;
                    }
                    this.rrr.Close();
                    if (this.pom1 <= 1)
                    {
                        this.nadjen_ul_id = this.pom2;
                        goto label_116;
                    }
                }
                this.ultek = this.apo_kajulica;
                this.komanda.CommandText = "select id from adm_ulice_daisy aud where substr('" + this.ultek + "',2,1) = '.' and  aud.aja_aja_id = " + Conversions.ToString(this.nadjen_mj_id) + " and  aud.naziv like substr('" + this.ultek + "',1,1) || '% ' || trim(substr('" + this.ultek + "',3)) ";
                this.rrr = this.komanda.ExecuteReader();
                if (!this.rrr.HasRows)
                {
                    this.rrr.Close();
                }
                else
                {
                    this.pom1 = 0;
                    while (this.rrr.Read())
                    {
                        checked { ++this.pom1; }
                        if (this.pom1 <= 1)
                            this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                        else
                            break;
                    }
                    this.rrr.Close();
                    if (this.pom1 <= 1)
                    {
                        this.nadjen_ul_id = this.pom2;
                        goto label_116;
                    }
                }
                this.ultek2 = "";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "ŠTROSMAYEROVA", false) == 0)
                    this.ultek2 = "JOSIPA JURJA STROSSMAYERA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "ŠTROSSMAYEROVA", false) == 0)
                    this.ultek2 = "JOSIPA JURJA STROSSMAYERA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "STROSSMAYEROVA", false) == 0)
                    this.ultek2 = "JOSIPA JURJA STROSSMAYERA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "J.J.STROSSMAYERA", false) == 0)
                    this.ultek2 = "JOSIPA JURJA STROSSMAYERA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "BRAĆE RADIĆA", false) == 0)
                    this.ultek2 = "BRAĆE RADIĆ";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "N.Š.ZRINSKOG", false) == 0)
                    this.ultek2 = "NIKOLE ŠUBIĆA ZRINSKOG";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "LJ.GAJA", false) == 0)
                    this.ultek2 = "LJUDEVITA GAJA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "SUPILOVA", false) == 0)
                    this.ultek2 = "FRANA SUPILA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "I.G.KOVAČIĆA", false) == 0)
                    this.ultek2 = "IVANA GORANA KOVAČIĆA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "BANA J.JELAČIĆA", false) == 0)
                    this.ultek2 = "BANA JOSIPA JELAČIĆA";
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.ultek, "A.G.MATOŠA", false) == 0)
                    this.ultek2 = "ANTUNA GUSTAVA MATOŠA";
                this.komanda.CommandText = "select id from adm_ulice_daisy aud where naziv = '" + this.ultek2 + "' and aja_aja_id = " + Conversions.ToString(this.nadjen_mj_id);
                this.rrr = this.komanda.ExecuteReader();
                if (!this.rrr.HasRows)
                {
                    this.rrr.Close();
                }
                else
                {
                    this.pom1 = 0;
                    while (this.rrr.Read())
                    {
                        checked { ++this.pom1; }
                        if (this.pom1 <= 1)
                            this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                        else
                            break;
                    }
                    this.rrr.Close();
                    if (this.pom1 <= 1)
                    {
                        this.nadjen_ul_id = this.pom2;
                        goto label_116;
                    }
                }
                if (this.ultek.Length > 3)
                {
                    this.komanda.CommandText = "select id from adm_ulice_daisy aud where aud.aja_aja_id = " + Conversions.ToString(this.nadjen_mj_id) + " and substr('" + this.ultek + "'," + Conversions.ToString(checked(this.ultek.Length - 2)) + ",3) in ('OVA', 'EVA') and  aud.naziv like '% " + this.ultek.Substring(0, checked(this.ultek.Length - 3)) + "A'";
                    this.rrr = this.komanda.ExecuteReader();
                    if (!this.rrr.HasRows)
                    {
                        this.rrr.Close();
                    }
                    else
                    {
                        this.pom1 = 0;
                        while (this.rrr.Read())
                        {
                            checked { ++this.pom1; }
                            if (this.pom1 <= 1)
                                this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                            else
                                break;
                        }
                        this.rrr.Close();
                        if (this.pom1 <= 1)
                        {
                            this.nadjen_ul_id = this.pom2;
                            goto label_116;
                        }
                    }
                }
                this.komanda.CommandText = "select sifra from adm_ulice_popularne aup, adm_ulice_daisy aud where aup.naziv = '" + this.ultek + "' and aud.id = aup.sifra and aud.aja_aja_id = " + Conversions.ToString(this.nadjen_mj_id);
                this.rrr = this.komanda.ExecuteReader();
                if (!this.rrr.HasRows)
                {
                    this.rrr.Close();
                }
                else
                {
                    this.pom1 = 0;
                    while (this.rrr.Read())
                    {
                        checked { ++this.pom1; }
                        if (this.pom1 <= 1)
                            this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                        else
                            break;
                    }
                    this.rrr.Close();
                    if (this.pom1 <= 1)
                    {
                        this.nadjen_ul_id = this.pom2;
                        goto label_116;
                    }
                }
                this.komanda.CommandText = "select adm_ul_id from mob_upar_sifre  mobus where mobus.mjtxt = '" + this.zadan_mjnaz + "' and mobus.id_dostavne_poste = '" + this.zadan_pttbr + "' and mobus.ultxt = '" + this.ultek + "' ";
                this.rrr = this.komanda.ExecuteReader();
                if (!this.rrr.HasRows)
                {
                    this.rrr.Close();
                }
                else
                {
                    this.pom1 = 0;
                    while (this.rrr.Read())
                    {
                        checked { ++this.pom1; }
                        if (this.pom1 <= 1)
                            this.pom2 = Conversions.ToInteger(this.rrr.GetValue(0));
                        else
                            break;
                    }
                    this.rrr.Close();
                    if (this.pom1 <= 1 && this.pom2 != 0)
                    {
                        this.nadjen_ul_id = this.pom2;
                        goto label_116;
                    }
                }
                this.nadjen_ul_id = checked(this.nadjen_mj_id + 5000000);
                this.komanda.CommandText = "select count(*) from mob_upar_sifre  mobus where mobus.mjtxt = '" + this.zadan_mjnaz + "' and mobus.id_dostavne_poste = '" + this.zadan_pttbr + "' and mobus.ultxt = '" + this.ultek + "' ";
                this.pom4 = Conversions.ToInteger(this.komanda.ExecuteScalar());
                if (this.pom4 <= 0)
                {
                    this.pom4 = this.pom4;
                    this.komanda.CommandText = "insert into mob_upar_sifre (mjtxt, id_dostavne_poste, adm_mj_id, ultxt, adm_ul_id, datum_inserta, imeop, primjer_tel) values ('" + this.zadan_mjnaz + "', '" + this.zadan_pttbr + "', " + Conversions.ToString(this.nadjen_mj_id) + ", '" + this.apo_kajulica + "', 0, sysdate, '" + this.tekuci_op_naziv + "', '" + this.kajtel + "') ";
                    this.komanda.ExecuteNonQuery();
                }
            }
            label_116:
            if (this.nadjen_mj_id == 0)
            {
                if (this.zadan_pttbr.Length < 4)
                {
                    this.nadjen_mj_id = 4999901;
                }
                else
                {
                    this.nadjen_mj_id = checked(this.ptt_tkc(this.zadan_pttbr.Substring(0, 2)) + 4999900);
                    if (this.nadjen_mj_id == 4999900)
                        this.nadjen_mj_id = 4999901;
                }
                this.nadjen_ul_id = checked(this.nadjen_mj_id + 5000000);
            }
            else
            {
                if (this.nadjen_ul_id != 0)
                    return;
                this.nadjen_ul_id = checked(this.nadjen_mj_id + 5000000);
            }
        }
