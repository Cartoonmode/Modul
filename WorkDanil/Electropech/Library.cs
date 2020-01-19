using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkDanil.Electropech
{
    public class Library
    {
        #region Входные парамаетры
        /// <summary>
        /// Начальная температура°C
        /// </summary>

        public double tmo { set; get; }

        /// <summary>
        /// Количество слитков
        /// </summary>

        public double n { set; get; }

        /// <summary>
        /// Конечная температура°C
        /// </summary>  

        public double tkon { set; get; }

        /// <summary>
        ///Длина слитка м и диаметр слитка
        /// </summary>

        public double Lsl { set; get; }

        public double Dsl { set; get; }

        /// <summary>
        ///Масса одного слитка 
        /// </summary>

        public double g1 { set; get; }

        /// <summary>
        /// Длина, диаметр и высота печи м
        /// </summary>

        public double Lpeh { set; get; }

        public double Dpeh { set; get; }

        public double Hpeh { set; get; }

        /// <summary>
        /// первое и второе время нагрева с
        /// </summary>

        public double T1 { set; get; }

        public double T2 { set; get; }

        /// <summary>
        /// Степень черноты слитков и кладки
        /// </summary>

        public double Em { set; get; }

        public double Ekl { set; get; }

        /// <summary>
        /// Угловой коэффициент с металла на металл
        /// </summary>

        public double fm_m { set; get; }

        /// <summary>
        /// Эффективная температура печи °C
        /// </summary>

        public double tef { set; get; }

        /// <summary>
        /// КПД
        /// </summary>

        public double ny { set; get; }

        #endregion

        #region  Рассчётные данные

        /// <summary>
        /// Общая масса садки составляет Gм
        /// </summary>

        public  double Gm { set; get; }

        /// <summary>
        /// Общее время подогрева
        /// </summary>

        public  double Tob { set; get; }

        /// <summary>
        /// Производительность печи
        /// </summary>

        public  double Ppeh { set; get; }

        /// <summary>
        /// Удельная производительность
        /// </summary>

        public  double pydel { set; get; }

        /// <summary>
        /// Требуемая полезная мощность в первом периоде нагрева
        /// </summary>

        public  double Npol { set; get; }

        /// <summary>
        /// Эффективная поверхность кладки
        /// </summary>

        public  double Fk { set; get; }

        /// <summary>
        /// Площадь пода печи
        /// </summary>

        public  double Fp { set; get; }

        /// <summary>
        /// Бокавая поверхность Fм
        /// </summary>

        public  double Fm { set; get; }

        /// <summary>
        /// Эффективная поверхность кладки ɸкл.м
        /// </summary>

        public  double fkl_m { set; get; }

        /// <summary>
        /// Коэффициент излучения
        /// </summary>

        public  double Cpr { set; get; }

        /// <summary>
        /// Угловной коэффициент с металлка на кладку ɸм.кл
        /// </summary>

        public  double fm_kl { set; get; }

        /// <summary>
        /// Температура поверхности tп1 в конце первого периода
        /// </summary>

        public  double tp1 { set; get; }

        /// <summary>
        /// Удельный тепловой поток на металл в конце первого периода qм1
        /// </summary>

        public  double qm1 { set; get; }

        /// <summary>
        /// Коэффициент лучистого теплообмена 2 периода ɑл2
        /// </summary>

        public  double al2 { set; get; }

        /// <summary>
        /// Полезный теполовой поток во втромо периоде qм2
        /// </summary>

        public  double qm2 { set; get; }

        /// <summary>
        /// Средний полезный тепловой поток на металл во втромо периоде qм II.ср
        /// </summary>

        public  double qmII_cr { set; get; }

        /// <summary>
        /// Средняя полезная тепловая мощность во втором периоде Nпол II.ср
        /// </summary>

        public  double NpolII_cr { set; get; }

        /// <summary>
        /// Средняя полезная тепловая мощность за весь нагрев Nпол.ср
        /// </summary>

        public  double N_all_pol_cr { set; get; }

        /// <summary>
        /// Затраченная мощность Nзатр
        /// </summary>

        public  double Nzatr { set; get; }

        /// <summary>
        /// Удельный расход электрэнергии Вэ
        /// </summary>

        public  double Bel { set; get; }

        #endregion

        #region Расчет

        public void Calculate()

        {
            CalculateForm();
        }

        public void CalculateForm()
        {
            /// Рассчётные формулы 
            Gm = n * g1;
            Tob = T1 + T2;
            Ppeh = Gm / Tob;
            Fp = Dpeh * Lpeh;
            pydel = Ppeh / Fp;
            Fm = n * 3.14 * Dsl * Lsl;
            fm_kl = 1.0 - fm_m;
            Fk = 2.0 * Hpeh * (Dpeh + Lpeh) + 2.0 * Dpeh * Lpeh;
            fkl_m = Fm / Fk;
            Cpr = 5.7 / ((1.0 / Em - 1.0) * fm_kl + 1.0 + (1.0 / Ekl - 1.0) * fkl_m);
            tp1 = 0.85 * tkon;
            qm1 = Cpr * (Math.Pow(((tef + 273) / 100.0), 4.0) - (Math.Pow(((tp1 + 273) / 100.0), 4.0))) * fm_kl;
            Npol = Math.Round((qm1 * Fm / 1000.0),0);
            al2 = Cpr * (Math.Pow(((tef + 273) / 100.0), 4.0) - (Math.Pow(((tkon + 273) / 100.0), 4.0))) * fm_kl / (tef - tkon);
            qm2 = al2 * (tef - tkon);
            qmII_cr = (qm1 - qm2) / Math.Log(qm1 / qm2);
            NpolII_cr = Math.Round((qmII_cr * Fm / 1000.0),0);
            N_all_pol_cr = Math.Round(((Npol * T1 + NpolII_cr * T2) / (T1 + T2)),0);
            Nzatr = Math.Round((N_all_pol_cr / ny),0);
            Bel = Math.Round((Nzatr / Ppeh * 1000.0),0);
        }

        public Models.Result Rachet()
        {
            return new Models.Result
            {
                /// Формулы

                _Tob = (double)Tob,
                _Gm = (double)Gm,
                _Ppeh = (double)Ppeh,
                _Fp = (double)Fp,
                _pydel = (double)pydel,
                _Fm = (double)Fm,
                _Fk = (double)Fk,
                _fkl_m = (double)fkl_m,
                _fm_kl = (double)fm_kl,
                _Cpr = (double)Cpr,
                _tp1 = (double)tp1,
                _qm1 = (double)qm1,
                _Npol = (double)Npol,
                _al2 = (double)al2,
                _qm2 = (double)qm2,
                _qmII_cr = (double)qmII_cr,
                _NpolII_cr = (double)NpolII_cr,
                _N_all_pol_cr = (double)N_all_pol_cr,
                _Nzatr = (double)Nzatr,
                _Bel = (double)Bel,
            };
        }
        #endregion

    }
}
