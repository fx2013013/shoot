using shootModels.General;
using shootModels.Items;
using shootModels.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shootModels
{
    #region 玩家
    [Serializable]
    /// <summary>
    /// 玩家英雄类
    /// </summary>
    public class Hero : SpaceShip
    {
        #region 参数
        private string str = "";
        /// <summary>
        /// 得分
        /// </summary>
        public int score = 0;
        private Image img = global::shootModels.Properties.Resources.hero;
        //用户是否按下"上\下\左\右"
        private bool U = false, D = false, L = false, R = false;
        public BlooBar blb = null;

        #endregion

        public Hero(int x, int y, bool faction, int width, int height, int speed, int life) : base(x, y, faction, width, height, speed, life)
        {
            blb = new BlooBar(x, y, life, width);
            SetShootBehavior(new ShootOneBulletByHero(this));
        }
        #region
        /// <summary>
        /// 按下按键
        /// </summary>
        /// <param name="e"></param>
        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    U = true;
                    break;
                case Keys.S:
                    D = true;
                    break;
                case Keys.A:
                    L = true;
                    break;
                case Keys.D:
                    R = true;
                    break;
                default: break;
            }
            ConfirmRolesDirection();
        }
        /// <summary>
        /// 按键抬起
        /// </summary>
        /// <param name="e"></param>
        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    U = false;
                    break;
                case Keys.S:
                    D = false;
                    break;
                case Keys.A:
                    L = false;
                    break;
                case Keys.D:
                    R = false;
                    break;
                case Keys.Space://发射子弹(现在改为只发射一颗子弹)
                    {

                        Fire();
                    }
                    break;

                default: break;
            }
            ConfirmRolesDirection();
        }

        /// <summary>
        /// 组合移动
        /// </summary>
        private void ConfirmRolesDirection()
        {
            if (!L && U && !R && !D)
                Direction = SpaceShipDirection.U;
            else if (!L && U && R && !D)
                Direction = SpaceShipDirection.RU;
            else if (!L && !U && R && !D)
                Direction = SpaceShipDirection.R;
            else if (!L && !U && R && D)
                Direction = SpaceShipDirection.RD;
            else if (!L && !U && !R && D)
                Direction = SpaceShipDirection.D;
            else if (L && !U && !R && D)
                Direction = SpaceShipDirection.LD;
            else if (L && !U && !R && !D)
                Direction = SpaceShipDirection.L;
            else if (L && U && !R && !D)
                Direction = SpaceShipDirection.LU;
            else if (!L && !U && !R && !D)
                Direction = SpaceShipDirection.STOP;
        }

        public override void Draw(Graphics g)
        {
            blb.NowLife = Math.Min(Life, blb.AllLife);
            Life = blb.NowLife;

            if (!Live)
            {
                return;
            }
            Move();
            blb.Draw(g);
            g.DrawString(str, new Font("Arial", 10f), new SolidBrush(Color.Red), X, Y - 50);
            g.DrawString("scores:", new Font("Arial", 10f), new SolidBrush(Color.Red), X, Y + 20 + img.Height);
            g.DrawString(score.ToString(), new Font("Arial", 10f), new SolidBrush(Color.Red), X + 55, Y + 20 + img.Height);
            base.Draw(g, img, X, Y);
        }

        protected override void Move()
        {
            int frmWidth = int.Parse(UpdateManager.getAtt("width"));
            int frmHeight = int.Parse(UpdateManager.getAtt("height"));
            base.Move();

            //超出边界检测
            if (X < 0) X = 0;
            if (Y < 0) Y = 0;
            if (X + WIDTH + 18 > frmWidth)
                X = frmWidth - 18 - this.WIDTH;
            if (Y + HEIGHT +37 > frmHeight)
                Y = frmHeight - 37 - this.HEIGHT;
            blb.X = X;
            blb.Y = Y;
        }

        /// <summary>
        /// 拾取Buff
        /// </summary>
        /// <param name="b"></param>
        public void AddBuff(Buff b)
        {
            if(b.Name == "hp")
            {
                Injury(-30);
            }
            else
            {
                if(ShootBehavior is ShootOneBulletByHero)
                    ShootBehavior = new ShootTwoBulletByHero(this);
                else if(ShootBehavior is ShootTwoBulletByHero)
                    ShootBehavior = new ShootThreeBulletByHero(this);
            }
        }
        #endregion
    }
    #endregion
}
