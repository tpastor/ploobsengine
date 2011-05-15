using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Entity;
using PloobsEngine.Input;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.SceneControl;
using PloobsEngine.Trigger;
using PloobsEngine.MessageSystem;
using PloobsEngine.Utils;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{   
    /// <summary>
    /// Trigger Screen
    /// Jogue bolas no cilo ;)
    /// </summary>
    public class TriggerBepuScreen : IScene
    {        
        ICamera cam;
        LightThrowBepu lt;
        ReciveMessage rmessage;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.98f, true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            ///Adicionando Componente de Mensagens 
            ///Utilizado pelo Trigger
            MessageDeliver md = new MessageDeliver();
            engine.AddComponent(md);         
        }


        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);                        

            #region Trigger
            ///Criacao de um Trigger
            {
                ///Modelo cujo formato sera utilizado para disparar o trigger
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cubo" );
                sm.SetTexture(factory.CreateTexture2DColor(1,1, Color.Red), TextureType.DIFFUSE); ///VERMELHO
                
                ///Criacao do Triangle Mesh
                TriangleMeshObject tm = new TriangleMeshObject(sm,new Vector3(200, 5, 0),Matrix.Identity,Vector3.One * 10,MaterialDescription.DefaultBepuMaterial());
                ///Criacao do Evento q sera disparado qd o trigger for acionado
                ///Pode-se criar outros tipos de eventos, basta extender a classe  IEvent
                ///O parametro passado eh o canal em que o trigger enviara suas mensagens
                ///Qualquer Entidade que extende IRecieveMessageEntity pode receber mensagens
                TriggerEvent te = new TriggerEvent("TriggerEvent","TriggerTest1");
                ///Criacao do Trigger
                BepuPhysicWorld physicWorld = this.World.PhysicWorld as BepuPhysicWorld;
                System.Diagnostics.Debug.Assert(physicWorld != null);
                BepuTrigger bt = new BepuTrigger(physicWorld, tm, te, true, true, true, true);
                ///Adiciona o trigger ao mundo
                this.World.AddTrigger(bt);

                ///Adicona um objeto na posicao do trigger (Objeto FANTASMA, nao sera detectado pelo trigger)
                ///Facilita na localizacao do Trigger (cilos brancos)
                GhostObject ghost = new GhostObject(new Vector3(200, 5, 0), Matrix.Identity,Vector3.One * 10);
                DeferredNormalShader shader = new DeferredNormalShader();
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, ghost);
                this.World.AddObject(obj3);      
            }


            #endregion

            ///Criacao de um Objeto q recebera mensagens do trigger
            #region Models
            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cubo");
                sm.SetTexture(factory.CreateTexture2DColor(1,1, Color.White), TextureType.DIFFUSE); ///BRANCO                
                IPhysicObject pi = new TriangleMeshObject( sm,new Vector3(20,50,50), Matrix.Identity,Vector3.One * 10,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();                                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                ///Cadastra uma funcao para tratar das mensagens recebidas
                ///existem outras maneiras de fazer isso, como Extender a classe IObject e sobrescrever o metodo HandleMessage
                obj3.OnRecieveMessage += new OnRecieveMessage(obj3_OnRecieveMessage);
                this.World.AddObject(obj3);
                ///Adiciona o Objeto criado ao grupo "TriggerEvent" que recebera as mensagens do trigger
                EntityMapper.getInstance().AddgrouptagRecieveEntity("TriggerEvent", obj3);
            }


            ///Cenario de sempre
            {
                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\cenario");                
                IPhysicObject pi = new TriangleMeshObject(sm,Vector3.Zero,Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }
            

            #endregion        
    
            ///Demonstracao de que qualquer entidade pode receber mensagens
            ///Olhar a implementacao desta classe, esta no fim deste arquivo
            rmessage = new ReciveMessage();

            cam = new CameraFirstPerson(GraphicInfo.Viewport);            
            cam.FarPlane = 2000;

            lt = new LightThrowBepu(this.World, factory);

            #region NormalLight
            ///Conjunto de luzes direcionais
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.4f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            #endregion

            this.World.CameraManager.AddCamera(cam);
            
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Trigger (BEPU)", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Launch balls at the red box", new Vector2(GraphicInfo.Viewport.Width - 315, 40), Color.White, Matrix.Identity);
            
            if (shouldDraw)
            {
                render.RenderTextComplete("Objeto: Trigger acionado " + mesSend.Cod + " Objeto " + triggerSend.ContactEntity.Name, new Vector2(20, 40), Color.White, Matrix.Identity);
                render.RenderTextComplete("RecieveMessage: Trigger acionado " + rmessage.mesRec.Cod + " Objeto " + triggerSend.ContactEntity.Name, new Vector2(20, 65), Color.White, Matrix.Identity);
            }            
        }

        bool shouldDraw;
        Message mesSend = null;
        BepuTrigger triggerSend = null;
        /// <summary>
        /// Chamado qd uma mensagem chega
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="mes"></param>
        void obj3_OnRecieveMessage(IObject Reciever, PloobsEngine.MessageSystem.Message mes)
        {
            ///Checa se eh a mensagem correta
            if (mes.SenderType == PloobsEngine.MessageSystem.SenderType.EVENT && mes.Tag == "TriggerEvent")
            {         
                BepuTrigger trigger = (BepuTrigger)mes.Content;
                mesSend = mes;
                triggerSend = trigger;
                shouldDraw = true;
            }
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("MessageDeliver");

            lt.CleanUp();
        }
    }

    
    public class ReciveMessage : IRecieveMessageEntity
    {
        public ReciveMessage()
        {
            ///Registra esta entidade
            EntityMapper.getInstance().AddEntity(this);
            ///Insere esta entidade em um canal (passara a receber mensagens deste canal)
            ///PARA ADICIONAR UMA ENTIDADE EM UM CANAL DEVE-SE ANTES REGISTRAR A ENTIDADE
            EntityMapper.getInstance().AddgrouptagRecieveEntity("TriggerEvent",this);
        }
        ~ReciveMessage()
        {
            ///DESREGISTRAR QD ENTIDADE DEIXA DE EXISTIR
            ///este metodo remove a entidade de todos os canais
            EntityMapper.getInstance().RemoveEntity(this);            
        }

        #region IRecieveMessageEntity Members

        public bool HandleThisMessageType(SenderType type)
        {
            return true;
        }

        public Message mesRec = null;
        public void HandleMessage(Message mes)
        {
            if (mes.SenderType == PloobsEngine.MessageSystem.SenderType.EVENT && mes.Tag == "TriggerEvent")
            {   
                BepuTrigger trigger = (BepuTrigger)mes.Content;
                mesRec = mes;
            }
        }

        #endregion

        #region IEntity Members
        int id;
        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        #endregion
    }
}

    