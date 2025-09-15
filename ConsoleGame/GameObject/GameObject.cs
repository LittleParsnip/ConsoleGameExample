using ConsoleGame.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleGame
{
    /// <summary>
    /// 游戏物体
    /// </summary>
    public class GameObject : IDisposable
    {
        // 世界尺寸
        public const int WORLD_WIDTH = Renderer.SCREEN_WIDTH;
        public const int WORLD_HEIGHT = Program.IS_SQUARE_FONT ? Renderer.SCREEN_HEIGHT : (Renderer.SCREEN_HEIGHT * 2);

        public static readonly GameObject Empty = new GameObject();

        #region 属性
        /// <summary>
        /// 位置向量
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
            }
        }
        /// <summary>
        /// 是否出界
        /// </summary>
        public bool IsOutOfBounds
        {
            get
            {
                return Position.X < 0 || Position.Y < 0 || Position.X >= WORLD_WIDTH || Position.Y >= WORLD_HEIGHT;
            }
        }
        /// <summary>
        /// 动画
        /// </summary>
        public Animation Animation { get; private set; } = new Animation();
        /// <summary>
        /// 图像
        /// </summary>
        public Image Image
        {
            get
            {
                Image image = Animation.GetCurrentFrame();

                // 物体坐标转换为图像坐标点
                image.Position = Position;
                image.SortingOrder = SortingOrder;
                return image;
            }
        }
        /// <summary>
        /// 渲染排序
        /// </summary>
        public int SortingOrder
        {
            get => sortingOrder;
            private set
            {
                sortingOrder = value;
            }
        }

        /// <summary>
        /// 碰撞目标图层列表（碰撞到列表中的图层时才调用OnCollide）
        /// </summary>
        protected virtual List<int> CollideTargetSortingOrder => new List<int>();
        #endregion

        #region 私有字段
        private Vector2 position = new Vector2();
        private int sortingOrder = 0;
        #endregion

        public GameObject()
        {
            EventSystem.RaiseGameObjectCreate(this, EventArgs.Empty);
            EventSystem.OnUpdate += EventSystem_OnUpdate;

            Init();
        }

        public GameObject(Vector2 position) : this()
        {
            Position = position;
        }

        public GameObject(Vector2 position, Image image) : this(position)
        {
            Animation.AddFrame(image);
        }

        public GameObject(Vector2 position, Animation anim) : this(position)
        {
            Animation.Dispose();
            Animation = anim;
        }

        public GameObject(Vector2 position, Image image, int sortingOrder) : this(position, image)
        {
            SetSortingOrder(sortingOrder);
        }

        public GameObject(Vector2 position, Animation anim, int sortingOrder) : this(position, anim)
        {
            SetSortingOrder(sortingOrder);
        }

        #region 事件
        private void EventSystem_OnUpdate(object? sender, EventArgs e)
        {
            Update(Time.DeltaSeconds);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 设置静态图像
        /// </summary>
        /// <param name="image">图像</param>
        public void SetImage(Image image)
        {
            Animation.ClearFrames();
            Animation.AddFrame(image);
        }

        /// <summary>
        /// 设置帧序列
        /// </summary>
        /// <param name="images">图像</param>
        public void SetImages(List<Image> images)
        {
            Animation.ClearFrames();
            foreach (Image i in images)
                Animation.AddFrame(i);
        }

        /// <summary>
        /// 重新设置动画
        /// </summary>
        /// <param name="anim"></param>
        public void SetAnimation(Animation anim)
        {
            Animation.Dispose();
            Animation = anim;
            Animation.Play();
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        public void SetPosition(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="position">位置</param>
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// 设置渲染顺序
        /// </summary>
        /// <param name="sortingOrder"></param>
        public void SetSortingOrder(int sortingOrder)
        {
            SortingOrder = sortingOrder;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            OnDestroy();
            EventSystem.OnUpdate -= EventSystem_OnUpdate;
            EventSystem.RaiseGameObjectDestroy(this, EventArgs.Empty);

            Animation.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 检测碰撞
        /// </summary>
        /// <param name="other"></param>
        public bool IsCollide(GameObject other)
        {
            // 暂时以图像重合作为碰撞检测依据
            foreach (Pixel thisPixel in Image.GetPixels())
            {
                foreach (Pixel otherPixel in other.Image.GetPixels())
                {
                    ScreenVector2 thisPixelPos = thisPixel.Position + (ScreenVector2)this.Position;
                    ScreenVector2 otherPixelPos = otherPixel.Position + (ScreenVector2)other.Position;

                    if (thisPixelPos == otherPixelPos)
                       return true;
                }
            }
            return false;
        }
        #endregion

        #region 虚函数
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            Animation.Play();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deltaSeconds"></param>
        protected virtual void Update(float deltaSeconds)
        {
            CollideUpdate();
        }

        /// <summary>
        /// 碰撞
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnCollide(GameObject other)
        {

        }

        /// <summary>
        /// 摧毁时
        /// </summary>
        protected virtual void OnDestroy()
        {

        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 碰撞更新
        /// </summary>
        private void CollideUpdate()
        {
            List<GameObject> gameObjectList = GameObjectPool.GetGameObjectList().ToList();
            foreach (GameObject g in gameObjectList)
            {
                // 不和自己碰撞
                if (g == this) continue;
                // 如果是目标类型就检测是否碰撞
                if (CollideTargetSortingOrder.Contains(g.SortingOrder))
                {
                    if (IsCollide(g))
                        OnCollide(g);
                }
            }
        }
        #endregion
    }
}
