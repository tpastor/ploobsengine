#define DEBUG

//Using statements. 
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PloobsEngine.Engine;


//Change this to the namespace of your project.
namespace PloobsEngine.Material
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    //QuadTerrain Class.
    //
    //This class generates a quadtree based terrain, complete with LOD and stitching. 
    //Comes with a construtor, update method and draw method.
    //Also comes with a shader.
    //
    //
    //Current problems:
    //All fixed. :)
    //
    //If you use this class, please mention me in your credits. It took 2 months for me to build, after all. :)
    //
    //If you have any questions, suggestions, modifications or random drunken abuse, please don't hesitate to contact me.
    //
    //Qu
    //Quasar
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    //Version 2.00
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    //Special Thanks
    //
    //Riemer Grootjans
    //Shawn Hargreaves
    //Jon Watte
    //Lord Ikon
    //Gorky
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Custom Vertex Type for terrain.
    /// Holds Position as a Vector3, as well as a sizeInBytes value.
    /// </summary>
    public struct VertexPosition
    {
        public Vector3 Position;

        public static int SizeInBytes = (3) * sizeof(float);
        public static VertexElement[] VertexElements = new VertexElement[]
        {
            new VertexElement( 0,VertexElementFormat.Vector3, VertexElementUsage.Position, 0 ),
        };

        public static VertexDeclaration VertexDeclaration = new VertexDeclaration(VertexElements);
    }

    /// <summary>
    /// Represents a QuadTree Terrain, complete with constructor method and LOD updating.
    /// Quasar.
    /// </summary>
    public class QuadTerrain
    {
        public void CleanUp()
        {

            foreach (var item in allVBs)
            {
                item.Dispose();
            }

            foreach (var item in allIBs)
            {
                item.Dispose();
            }

            allIndices = null;
            allVertices = null;
            if(normalTexture!=null)
                normalTexture.Dispose();            
         }

        ////////////////////////////////////////////////////////////////////
        //This two dimentional array contains all terrain height data, for later retreival.
        public float[,] HeightStore
        {
            get { return heightStore; }
        }
        private float[,] heightStore;

        //This is the same as the heightStore, but for vertex normals.
        public Vector3[,] NormStore
        {
            get { return normStore; }
        }
        private Vector3[,] normStore;

        //These are our terrain Vertex and Index arrays, and their corresponding buffers.
        private VertexPosition[][] allVertices;
        private int[][] allIndices;

        private VertexBuffer[] allVBs;
        private DynamicIndexBuffer[] allIBs;

         
        ////////////////////////////////////////////////////////////////////
        

        

        //These are taken from the heightmap, and are pretty self explanatory.
        public int TerrainWidth
        {
            get { return terrainWidth; }
        }
        private int terrainWidth;

        public int TerrainHeight
        {
            get { return terrainHeight; }
        }
        private int terrainHeight;

        //This is the heightmap texture in its most raw form.
        private Texture2D HeightMap;

        //This is the size of an individual LOD square. The entire quadTerrain is made up of nodes of this size.
        private int SquareSize;

        //These are the xy and height scaling factor for the entire terrain.
        public float flatScale
        {
            get { return Scale; }
        }
        private float Scale;
        public float verticalScale
        {
            get { return HeightScale; }
        }
        private float HeightScale;

        //This is the node depth of the most detailed LOD squares.
        private int maxNodeDepth;

        public float LODHeightImpact;

        //This list holds all the quadNodes in the entire tree. allQuadNodes[0] is always the root node.
        private List<QuadNode> allQuadNodes;
        //A pointer to the Root QuadNode.
        QuadNode RootNode;

        //The number of indices rendered for each vertex buffer
        public int[] Renderedindices
        {
            get { return renderedindices; }
        }
        private int[] renderedindices;

        //These relate to the statistics for individual Vertex Buffers
        private int VBsize;
        public int NumberOfVertexBuffers
        {
            get { return numberOfVBs; }
        }
        private int numberOfVBs;
        //The Square Root of the numberOfVBs is used surprisingly often, so I added it to a variable to phase out Math.Sqrt calls.
        private int sqrtNumberOfVBs;

        //The Global normal Texture.
        public Texture2D globalNormalTexture
        {
            get { return normalTexture; }
        }
        private Texture2D normalTexture;

        internal GraphicFactory factory;

        /// <summary>
        /// Initialises a complete QuadTerrain.
        ///This is the Constructor method. As you might have guessed, it constructs a quad terrain.
        /// Quasar.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="heightMap">The Texture2D to use as a heightmap. Must have square dimensions of (2^n)+1, where n is an integer.</param>
        /// <param name="squareSize">The edge size of each individual LOD square. Lower values increase CPU Load, decrease GPU Load, and increase Loading times. Must be (2^n)+1, and larger than 5.</param>
        /// <param name="vertexBufferSize">The size of Vertex buffer to use. Lower Values increase the number of draw calls by splitting the terrain into several Vertex Buffers. Must be (2^n)+1, and larger than squareSize.</param>
        /// <param name="scale">The XZ scale to multiply the terrain by.</param>
        /// <param name="height">The Y scale to multiply the terrain by.</param>
        public QuadTerrain(GraphicFactory factory ,Texture2D heightMap, int squareSize, int vertexBufferSize, float scale, float height)
        {
            this.factory = factory;
            //I'm not entirely sure what this does, but it is used in updateTerrain.
            //I think it is used to prevent the Vertex Buffers being filled during the initialisation UpdateTerrain call.
            first = true;

            LODHeightImpact = 1.0f;                        
             
            

            //Set terrain width and height from heightmap.
            terrainHeight = heightMap.Height;
            terrainWidth = heightMap.Width;

            //Set some obvious public variables
            HeightMap = heightMap;
            SquareSize = squareSize;
            Scale = scale;
            HeightScale = height;
            VBsize = vertexBufferSize;

            //Copy the heightmap from a Texture to an array of colours...
            Color[] heightMapColors = new Color[terrainWidth * terrainHeight];
            heightMap.GetData(heightMapColors);

            //Initialise the HeightStore and 
            heightStore = new float[heightMap.Width, heightMap.Height];
            normStore = new Vector3[heightMap.Width, heightMap.Height];

            //this is the Normal texture for the entire terrain. It is used within the shader to prevent normal popup.
            normalTexture = factory.CreateTexture2D(heightMap.Width, heightMap.Height, true, SurfaceFormat.Color);
            Color[] normalData = new Color[heightMap.Width * heightMap.Height];

            #region depricated
            /*
            int NodeDepth= 0;
            int NodeLevel = (int)Math.Pow(2, (NodeDepth));
            int NodeScale = ((heightMap.Height - 1) / NodeLevel) + 1;
            int stepSize = (NodeScale - 1) / (squareSize - 1);
            */

            ////////////////////////////////////////////////////
            // For:
            // SquareSize = 9
            // Height = 257
            // NodeDepth,   NodeLevel,  NodeScale,  stepSize
            // 0,           1,          257,        64
            // 1,           2,          129,        32
            // 2,           4,          65,         16
            // 3,           8,          33,         8
            // 4,           16,         19,         4
            // 5,           32,         9,          2
            // 6,           64,         5,          1
            ////////////////////////////////////////////////////

            //Get node depth:
            //int NodeScale = stepSize*(squareSize-1)+1;
            //int NodeLevel = (NodeScale - 1) * (heightMap.Height - 1);
            //int maxNodeDepth = (int)Math.Log(NodeLevel, 2);
            //int maxNodeDepth;
            #endregion

            //Determine Maximum Node Depth from Square Size and height of heightmap
            int NodeScale = 1 * (squareSize - 1) + 1;
            int NodeLevel = (heightMap.Height - 1) / (NodeScale - 1);
            maxNodeDepth = (int)Math.Log(NodeLevel, 2);

            //Work out number of vertex arrays needed:
            //Math.Pow(HeightMap.HEIGHT / 512, 2))+1;
            numberOfVBs = (int)Math.Pow((terrainHeight - 1) / (VBsize - 1), 2);
            sqrtNumberOfVBs = (int)Math.Sqrt(numberOfVBs);

            //Initialise the Array of Vertex Arrays, accompanying Array of Vertex Buffers and the Array of Integer Index Arrays.
            allVertices = new VertexPosition[numberOfVBs + 1][];
            allVBs = new VertexBuffer[numberOfVBs + 1];
            allIndices = new int[numberOfVBs + 1][];

            //And as if that wasn't confusing enough...
            //Now I initialise each Array in the Array of Vertex Arrays, each corresponding Buffer in the Array of Vertex Buffers,
            //and each Integer Index Array in the Array of Integer Index Arrays.
            for (int i = 0; i < numberOfVBs + 1; i++)
            {
                allVertices[i] = new VertexPosition[(VBsize + 1) * (VBsize + 1)];
                allVBs[i] = factory.CreateVertexBuffer(VertexPosition.VertexDeclaration, (VBsize + 1) * (VBsize + 1), BufferUsage.WriteOnly);
                allIndices[i] = new int[VBsize * VBsize * 6];
            }
            //Array.

            //This list is used to sort the Quadnodes to be drawn by distance to save on Overdraw.
            qnl = new List<QuadNode>();

            //Create a 2D array of floats from the heightmap colour array.
            for (int y = 0; y < terrainHeight; y++)
            {
                for (int x = 0; x < terrainWidth; x++)
                {
                    //heightstore Array
                    heightStore[x, y] = heightMapColors[x + y * terrainWidth].R;
                }
            }

            //Define the 'renderedIndices' array, which keeps track of the number of indices from each vertex buffer each frame.
            renderedindices = new int[numberOfVBs + 1];


            /////////////////////////////////////////////////////
            //Generate Vertex Positions and normals
            int PlusXVBIndex;
            int PlusYVBIndex;
            int PlusXYVBIndex;
            int PlusVBIndex;
            for (int y = 0; y < terrainHeight; y++)
            {
                for (int x = 0; x < terrainWidth; x++)
                {

                    PlusXYVBIndex = (int)(Math.Floor(((float)(x + 1) / terrainWidth) * sqrtNumberOfVBs) + (Math.Floor(((float)(y + 1) / terrainWidth) * sqrtNumberOfVBs) * sqrtNumberOfVBs));
                    PlusXVBIndex = (int)(Math.Floor(((float)(x + 1) / terrainWidth) * sqrtNumberOfVBs) + (Math.Floor(((float)(y) / terrainWidth) * sqrtNumberOfVBs) * sqrtNumberOfVBs));
                    PlusYVBIndex = (int)(Math.Floor(((float)(x) / terrainWidth) * sqrtNumberOfVBs) + (Math.Floor(((float)(y + 1) / terrainWidth) * sqrtNumberOfVBs) * sqrtNumberOfVBs));
                    PlusVBIndex = (int)(Math.Floor(((float)(x) / terrainWidth) * sqrtNumberOfVBs) + (Math.Floor(((float)(y) / terrainWidth) * sqrtNumberOfVBs) * sqrtNumberOfVBs));

                    Vector3 normX = Vector3.Zero;
                    Vector3 normY = Vector3.Zero;
                    Vector3 normalVector = new Vector3();

                    if (x > 0 && y > 0 && x < terrainWidth - 1 && y < terrainHeight - 1)
                    {
                        normX = new Vector3((heightStore[x - 1, y] - heightStore[x + 1, y]) / 2 * height, 0, scale);
                        normY = new Vector3(0, (heightStore[x, y - 1] - heightStore[x, y + 1]) / 2 * height, scale);
                        normalVector = normX + normY;
                        normalVector.Normalize();

                        Vector3 texVector = new Vector3();
                        texVector.X = (normalVector.X + 1) / 2f;
                        texVector.Y = (normalVector.Y + 1) / 2f;
                        texVector.Z = (normalVector.Z + 1) / 2f;
                        normalData[x + y * terrainHeight] = new Color(texVector);

                        //MessageBox.Show(normalVector.ToString() + " "+new Color(normalVector));
                    }
                    else
                    {
                        normX = new Vector3(0, 0, scale);
                        normY = new Vector3(0, 0, scale);
                        normalVector = normX + normY;
                        normalVector.Normalize();


                        Vector3 texVector = new Vector3();
                        texVector.X = (normalVector.X + 1) / 2f;
                        texVector.Y = (normalVector.Y + 1) / 2f;
                        texVector.Z = (normalVector.Z + 1) / 2f;
                        normalData[x + y * terrainHeight] = new Color(texVector);
                    }
                    normStore[x, y] = normalVector;

                    /////////////////////////////////////////////
                    //Fill Vertex Arrays
                    //Foreach vertex array...
                    for (int i = 0; i < numberOfVBs; i++)
                    {
                        //Vertex Buffers
                        // 0, 1, 2, 3... What about root 0?
                        // 4, 5, 6, 7
                        // 8, 9,10,11
                        //12,13,14,15
                        //Change to...
                        // 1, 2, 3, 4... root=0
                        // 5, 6, 7, 8
                        // 9,10,11,12
                        //13,14,15,16

                        //This works to sort the VB's. 
                        //It works to align the x,y and VBi values: only if an x/y coord is in an i VB. Uses 1st VB chart. Just use i+1
                        ///////////////////////////////////////////////////////////
                        //^^ If you understood the above comment, you're doing better than me. ^^
                        ///////////////////////////////////////////////////////////

                        //This is the 'multiple vertex buffers' algorithm, which puts vertices into their own VBuffers. Anything involving
                        //it won't be very well documented, because I can only vaguely remember writing it in the first place.

                        //I'm not entirely sure what that means, but I suspect I was either drunk or asleep at the time. Possibly both.
                        if (PlusXYVBIndex == i)
                        {
                            //allVertices[i+1][(x % VBsize) + (y % VBsize) * VBsize].Position = new Vector3(x * scale, heightStore[x, y] * height, -y * scale);
                            if (x < terrainHeight - 1)
                            {
                                //MessageBox.Show(x + " " + y + " " + x % (VBsize - 1) + " " + y % (VBsize - 1) + " " + i);

                                allVertices[i + 1][x % (VBsize - 1) + y % (VBsize - 1) * VBsize].Position = new Vector3(x * scale, heightStore[x, y] * height, y * scale);
                                //allVertices[i + 1][x % (VBsize - 1) + y % (VBsize - 1) * VBsize].Normal = normalVector;
                                //Add this vertex to VB i+1, at location (x % (VBsize-1) + y % (VBsize-1) * VBsize) 
                            }
                        }
                        else
                        {
                            //Bottom or left
                            if (PlusVBIndex == i)
                            {
                                if (PlusYVBIndex == i)
                                {
                                    //MessageBox.Show(x + "BotLeft&Y " + y + " " + (x % (VBsize - 1) + (VBsize - 1)) + " " + y % (VBsize - 1) + " " + i);  
                                    allVertices[i + 1][(x % (VBsize - 1) + (VBsize - 1)) + (y % (VBsize - 1)) * VBsize].Position = new Vector3(x * scale, heightStore[x, y] * height, y * scale);
                                    //allVertices[i + 1][(x % (VBsize - 1) + (VBsize - 1)) + (y % (VBsize - 1)) * VBsize].Normal = normalVector;

                                    //Add this vertex to VB i+1, at location (x % (VBsize - 1) + (VBsize - 1)) + (y % (VBsize - 1)) * VBsize)
                                }
                                else
                                {
                                    if (PlusXVBIndex == i)
                                    {
                                        //MessageBox.Show(x + "BotLeft&X " + y + " " + x % (VBsize - 1) + " " + (y % (VBsize - 1) + (VBsize - 1)) + " " + i);
                                        allVertices[i + 1][x % (VBsize - 1) + (y % (VBsize - 1) + (VBsize - 1)) * VBsize].Position = new Vector3(x * scale, heightStore[x, y] * height, y * scale);
                                        //allVertices[i + 1][x % (VBsize - 1) + (y % (VBsize - 1) + (VBsize - 1)) * VBsize].Normal = normalVector;

                                        //Add this vertex to VB i+1, at location (x % (VBsize - 1)) + (y % (VBsize - 1) + (VBsize - 1)) * VBsize)

                                    }
                                    else
                                    {
                                        //MessageBox.Show(x + "BotLeft " + y + " " + (x % (VBsize - 1) + (VBsize - 1)) + " " + (y % (VBsize - 1) + (VBsize - 1)) + " " + i);
                                        allVertices[i + 1][x % (VBsize - 1) + (VBsize - 1) + (y % (VBsize - 1) + (VBsize - 1)) * VBsize].Position = new Vector3(x * scale, heightStore[x, y] * height, y * scale);
                                        //allVertices[i + 1][x % (VBsize - 1) + (VBsize - 1) + (y % (VBsize - 1) + (VBsize - 1)) * VBsize].Normal = normalVector;
                                        //Add this vertex to VB i+1, at location (x % (VBsize - 1) + (VBsize - 1)) + (y % (VBsize - 1) + (VBsize - 1)) * VBsize)
                                    }
                                }
                            }
                            else
                            {
                                //Corner Left
                                if (PlusYVBIndex == i)
                                {
                                    if (y < terrainHeight - 1)
                                    {
                                        //MessageBox.Show(x + "Left " + y + " " + (x % (VBsize - 1) + (VBsize - 1)) + " " + y % (VBsize - 1) + " " + i);
                                        allVertices[i + 1][(x % (VBsize - 1) + (VBsize - 1) + (y % (VBsize - 1)) * VBsize)].Position = new Vector3(x * scale, heightStore[x, y] * height, y * scale);
                                        //allVertices[i + 1][(x % (VBsize - 1) + (VBsize - 1) + (y % (VBsize - 1)) * VBsize)].Normal = normalVector;
                                        //Add this vertex to VB i+1, at location ((x % (VBsize - 1) + (VBsize - 1)) + (y % (VBsize - 1)) * VBsize)
                                    }
                                }
                                //Corner Bottom
                                if (PlusXVBIndex == i)
                                {
                                    if (x < terrainHeight - 1)
                                    {
                                        //MessageBox.Show(x + "Bot " + y + " " + x % (VBsize - 1) + " " + (y % (VBsize - 1) + (VBsize - 1)) + " " + i);
                                        allVertices[i + 1][(x % (VBsize - 1)) + ((y % (VBsize - 1) + (VBsize - 1)) * VBsize)].Position = new Vector3(x * scale, heightStore[x, y] * height, y * scale);
                                        //allVertices[i + 1][(x % (VBsize - 1)) + ((y % (VBsize - 1) + (VBsize - 1)) * VBsize)].Normal = normalVector;
                                        //Add this vertex to VB i+1, at location ((x % (VBsize - 1)) + ((y % (VBsize - 1) + (VBsize - 1)) * VBsize)
                                    }
                                }
                            }
                        }


                        if (i == 0)
                        {
                            int stepSize = (terrainWidth - 1) / (vertexBufferSize - 1);
                            int numberOfSteps = (terrainWidth - 1) / stepSize;
                            if (x % stepSize == 0 && y % stepSize == 0)
                            {
                                allVertices[0][(x / stepSize) + (y / stepSize) * VBsize].Position = new Vector3(x * scale, heightStore[x, y] * height, y * scale);
                                //allVertices[0][(x / stepSize) + (y / stepSize) * VBsize].Normal = normalVector;
                                //Add this vertex to VB 0, at location ((x/stepSize) + (y/stepSize) * VBsize)
                            }
                        }
                    }
                }
            }
            //Generate the normal texture from the normal data generated a second ago.
            normalTexture.SetData<Color>(normalData);

            factory.MipMapTexture(ref normalTexture);            
                       

            ////////////////////////////////////////////////////////////
            //Add to vertex buffer
            for (int j = 0; j < numberOfVBs + 1; j++)
            {
                allVBs[j].SetData<VertexPosition>(allVertices[j]);
            }
            ////////////////////////////////////////////////////////////


            #region depricated
            // int[] nodeStatsArray = new int[6,6];
            //for (i=HeightMap.Height-1;i>

            //   rootNode = new QuadNode(HeightMap, SquareSize, 4, 0, 0);


            //NodeDepth     Nodes   Total Nodes
            //0             1       1
            //1             4       5
            //2             16      21
            //3             64      85
            //4             256     341
            //5             1024    1365

            //allQuadNodes = new List<QuadNode>();


            //int NodeDepth = 0;
            //NodeLevel = (int)Math.Pow(2, (NodeDepth));
            //NodeScale = ((heightMap.Height - 1) / NodeLevel) + 1;
            //int stepSize = (NodeScale - 1) / (squareSize - 1);
            //QuadNode qNode;
            //QuadNode[] parentNode = new QuadNode[1];

            //int xPosition = 0;
            //int yPosition = 0;
            //qNode = new QuadNode(HeightMap, SquareSize, NodeDepth, xPosition, yPosition);
            //NodeDepth++;

            //while (xPosition < terrainWidth)
            //{
            //    while (yPosition < terrainHeight)
            //    {
            //        while (NodeDepth < maxNodeDepth - 5)
            //        {
            //            NodeLevel = (int)Math.Pow(2, (NodeDepth));
            //            NodeScale = ((heightMap.Height - 1) / NodeLevel) + 1;
            //            qNode = new QuadNode(HeightMap, SquareSize, NodeDepth, xPosition, yPosition);
            //            allQuadNodes.Add(qNode);
            //            qNode = new QuadNode(HeightMap, SquareSize, NodeDepth, xPosition + NodeScale - 1, yPosition);
            //            allQuadNodes.Add(qNode);
            //            qNode = new QuadNode(HeightMap, SquareSize, NodeDepth, xPosition, yPosition + NodeScale - 1);
            //            allQuadNodes.Add(qNode);
            //            qNode = new QuadNode(HeightMap, SquareSize, NodeDepth, xPosition + NodeScale - 1, yPosition + NodeScale - 1);
            //            allQuadNodes.Add(qNode);
            //            parentNode[0] = qNode;
            //            NodeDepth++;
            //        }
            //        NodeDepth--;


            //        NodeLevel = (int)Math.Pow(2, (NodeDepth-1));
            //        NodeScale = ((heightMap.Height - 1) / NodeLevel) + 1;




            //        //NodeDepth--;
            //        yPosition += NodeScale - 1;
            //    }
            //    xPosition += NodeScale - 1;
            //    yPosition = 0;
            //}

            //NodeDepth++;


            //int numQuadNodes = allQuadNodes.Count;
            //int[] indexBufferArray = new int[numQuadNodes * ((squareSize - 1) * (squareSize - 1) * 6)];

            #endregion

            //Define Quadnode List
            allQuadNodes = new List<QuadNode>();

            //Create RootNode
            RootNode = new QuadNode(HeightMap, normStore, SquareSize, VBsize, Scale, 0, 0, 0, null, allVertices);
            allQuadNodes.Add(RootNode);


            ////////////////////////////////////////////////
            //Creates all quadnodes in the entire quadtree
            //
            //This is a recursive function. It breaks the rootnode into 4 new quadnodes, then applies itself to each of 
            //these children nodes, in turn breaking them. See it's definition for a more complete explanation.
            RecursiveCreateQuad(RootNode);
            ////////////////////////////////////////////////

            //Set Adjacent Nodes for each Quadnode, for stitching purposes.
            for (int i = 0; i < allQuadNodes.Count; i++)
            {
                if (allQuadNodes[i].NodeDepth > 0)
                {
                    foreach (QuadNode qNode in allQuadNodes)
                    {
                        if (qNode.XPosition == allQuadNodes[i].XPosition + allQuadNodes[i].NodeScale - 1 && qNode.NodeDepth == allQuadNodes[i].NodeDepth && qNode.YPosition == allQuadNodes[i].YPosition)
                        {
                            allQuadNodes[i].adjacentNorthQuad = qNode;
                        }
                        if (qNode.YPosition == allQuadNodes[i].YPosition + allQuadNodes[i].NodeScale - 1 && qNode.NodeDepth == allQuadNodes[i].NodeDepth && qNode.XPosition == allQuadNodes[i].XPosition)
                        {
                            allQuadNodes[i].adjacentEastQuad = qNode;
                        }
                        if (qNode.XPosition == allQuadNodes[i].XPosition - allQuadNodes[i].NodeScale + 1 && qNode.NodeDepth == allQuadNodes[i].NodeDepth && qNode.YPosition == allQuadNodes[i].YPosition)
                        {
                            allQuadNodes[i].adjacentSouthQuad = qNode;
                        }
                        if (qNode.YPosition == allQuadNodes[i].YPosition - allQuadNodes[i].NodeScale + 1 && qNode.NodeDepth == allQuadNodes[i].NodeDepth && qNode.XPosition == allQuadNodes[i].XPosition)
                        {
                            allQuadNodes[i].adjacentWestQuad = qNode;
                        }
                    }
                }
            }

            ///////////////////////////////////////////////
            //Run an update on the terrain for it's initial state. See the update method.
            //I'm not entirely sure why this is necessary, but I'm sure there's a good reason.
            Matrix viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 10f, 1, 100);
            BoundingFrustum startFrustrum = new BoundingFrustum(viewMatrix * projectionMatrix);
#if DEBUG
            debugTimer = new Stopwatch();
#endif

            UpdateTerrain(new Vector3(), startFrustrum, 3.5f);
            allIBs = new DynamicIndexBuffer[allIndices.Length];
            for (int l = 0; l < allIndices.Length; l++)
            {
                allIBs[l] = factory.CreateDynamicIndexBuffer(IndexElementSize.ThirtyTwoBits, (allVertices[l].Length), BufferUsage.WriteOnly);
            }
        }
        
        //////////////////////////////////////////////
        /// <summary>
        /// This function creates a quad nodes four component children. These child nodes then have this function applied to them. 
        /// This results in a recursion down the quad tree, creating nodes until maxNodeDepth is reached.
        /// </summary>
        /// <param name="rootNode">The node to start the recursion at.</param>
        private void RecursiveCreateQuad(QuadNode rootNode)
        {
            //NOW WITH ANNOYING FRUSTRUMS!
            //^Now that I've got them working, they're not annoying anymore. But they're still frustrums ^-^
            //Generate LeftUp child Node, set it's parent, and add it to the Quadnode list.
            rootNode.leftUpNode = new QuadNode(HeightMap, normStore, SquareSize, VBsize, Scale, rootNode.NodeDepth + 1, rootNode.XPosition, rootNode.YPosition, rootNode, allVertices);
            rootNode.leftUpNode.parent = rootNode;
            allQuadNodes.Add(rootNode.leftUpNode);

            //Generate leftDown child Node, set it's parent, and add it to the Quadnode list.
            rootNode.leftDownNode = new QuadNode(HeightMap, normStore, SquareSize, VBsize, Scale, rootNode.NodeDepth + 1, rootNode.XPosition + rootNode.NodeScale / 2, rootNode.YPosition, rootNode, allVertices);
            rootNode.leftDownNode.parent = rootNode;
            allQuadNodes.Add(rootNode.leftDownNode);

            //Generate rightUp child Node, set it's parent, and add it to the Quadnode list.
            rootNode.rightUpNode = new QuadNode(HeightMap, normStore, SquareSize, VBsize, Scale, rootNode.NodeDepth + 1, rootNode.XPosition, rootNode.YPosition + rootNode.NodeScale / 2, rootNode, allVertices);
            rootNode.rightUpNode.parent = rootNode;
            allQuadNodes.Add(rootNode.rightUpNode);

            //Generate rightDown child Node, set it's parent, and add it to the Quadnode list.
            rootNode.rightDownNode = new QuadNode(HeightMap, normStore, SquareSize, VBsize, Scale, rootNode.NodeDepth + 1, rootNode.XPosition + rootNode.NodeScale / 2, rootNode.YPosition + rootNode.NodeScale / 2, rootNode, allVertices);
            rootNode.rightDownNode.parent = rootNode;
            allQuadNodes.Add(rootNode.rightDownNode);


            //Check that this isn't as deep as the recursion is allowed to go...
            if (rootNode.NodeDepth < maxNodeDepth - 1)
            {
                //And recurse on all 4 nodes.
                RecursiveCreateQuad(rootNode.leftUpNode);
                RecursiveCreateQuad(rootNode.leftDownNode);
                RecursiveCreateQuad(rootNode.rightUpNode);
                RecursiveCreateQuad(rootNode.rightDownNode);
            }
        }

        //////////////////////////////////////////////////
        /// <summary>
        /// Another recursive Function, this was previously used to set the status of a node and then recurse up the tree, setting 
        /// the status of all it's parent nodes. 
        /// 
        /// Now depricated, because this is clearly a retarded way to do things (the whole point of a quad tree in to recurse *down*, this 
        /// function is kept because it may contain useful code I can draw on later.
        /// </summary>
        /// <param name="qNode">A leaf node</param>
        private void RecursiveParentStatus(QuadNode qNode)
        {
            if (qNode.NodeDepth > 0)
            {
                short tempStatus1 = qNode.parent.leftUpNode.status;
                short tempStatus2 = qNode.parent.leftDownNode.status;
                short tempStatus3 = qNode.parent.rightUpNode.status;
                short tempStatus4 = qNode.parent.rightDownNode.status;


                qNode.parent.status = 0;
                if (qNode.status == 1)
                {
                    if (qNode.leftUpNode == null)
                    {
                        qNode.parent.leftUpNode.status = 1;
                        qNode.parent.rightUpNode.status = 1;
                        qNode.parent.leftDownNode.status = 1;
                        qNode.parent.rightDownNode.status = 1;
                        qNode.parent.status = 2;
                    }
                    else
                    {
#if WINDOWS
                        System.Diagnostics.Debug.Fail("debug problem1");
#else
                        throw new Exception("debug problem1");
#endif
                    }
                }
                else
                {
                    if (qNode.status == 2)
                    {
                        if (qNode.parent.leftUpNode.status == 0)
                        {
                            qNode.parent.leftUpNode.status = 1;
                        }
                        if (qNode.parent.leftDownNode.status == 0)
                        {
                            qNode.parent.leftDownNode.status = 1;
                        }
                        if (qNode.parent.rightUpNode.status == 0)
                        {
                            qNode.parent.rightUpNode.status = 1;
                        }
                        if (qNode.parent.rightDownNode.status == 0)
                        {
                            qNode.parent.rightDownNode.status = 1;
                        }
                        qNode.parent.status = 2;
                    }
                    else
                    {
#if WINDOWS
                        System.Diagnostics.Debug.Fail("debug problem2");
#else
                        throw new Exception("debug problem2");
#endif
                    }
                }


                RecursiveParentStatus(qNode.parent);
            }
        }

        ////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Yet another recursive function. This function takes the position of the camera, and statuses all the children nodes of the 
        /// specified quadnode based on their distance. It then recurses down the tree, statusing as it goes.
        /// In essence, this function is what applies our LOD
        /// 
        /// Addition: I've integrated the frustrum checks into this algorithm, to improve performance.
        /// </summary>
        /// <param name="qNode">The root node.</param>
        /// <param name="cameraPoint">The camera's position</param>
        /// <param name="LODLevel">The LOD Distance to apply. 3.5 is the recommended minimum, because of stitching issues. Less than 2 may cause an unhandled exception.</param>
        private void RecursiveChildStatus(QuadNode qNode, Vector3 cameraPoint, float LODLevel, BoundingFrustum CameraFrustrum)
        {
            //Determine the squared distance of the camera from the specified node, taking into account the size of the node and the scale of 
            //the terrain.
            //This value doesn't undergo a Sqrt to save processing power: instead, the opposite side is sqared
            qNode.distanceFromCamera = (float)(Math.Pow(Math.Abs(cameraPoint.X - ((qNode.XPosition + (qNode.NodeScale / 2)) * Scale)), 2) + Math.Pow(Math.Abs(cameraPoint.Y - ((0 + (qNode.NodeScale / 2)) * Scale)), 2) * LODHeightImpact + Math.Pow(Math.Abs(cameraPoint.Z - ((qNode.YPosition + (qNode.NodeScale / 2)) * Scale)), 2));

            /////////////////////////////////////////////////
            //Staus this node as 1...
            qNode.status = 1;
            //...then, if node depth is not too deep...
            if (qNode.NodeDepth < maxNodeDepth)
            {
                float leftUpLOD = qNode.leftUpNode.NodeScale * Scale * LODLevel;
                float leftDownLOD = qNode.leftDownNode.NodeScale * Scale * LODLevel;
                float rightUpLOD = qNode.rightUpNode.NodeScale * Scale * LODLevel;
                float rightDownLOD = qNode.rightDownNode.NodeScale * Scale * LODLevel;

                leftUpLOD *= leftUpLOD;
                leftDownLOD *= leftDownLOD;
                rightUpLOD *= rightUpLOD;
                rightDownLOD *= rightDownLOD;

                //...determine whether or not to recurse onto the nodes children.
                if (qNode.distanceFromCamera < leftUpLOD)
                {
                    if (CameraFrustrum.Intersects(qNode.leftUpNode.boundBox))
                    {
                        qNode.status = 2;
                        RecursiveChildStatus(qNode.leftUpNode, cameraPoint, LODLevel, CameraFrustrum);
                    }
                    else
                    {
                        qNode.leftUpNode.inView = false;
                        qNode.status = 2;
                    }
                }
                if (qNode.distanceFromCamera < leftDownLOD)
                {
                    if (CameraFrustrum.Intersects(qNode.leftDownNode.boundBox))
                    {
                        qNode.status = 2;
                        RecursiveChildStatus(qNode.leftDownNode, cameraPoint, LODLevel, CameraFrustrum);
                    }
                    else
                    {
                        qNode.leftDownNode.inView = false;
                        qNode.status = 2;
                    }
                }
                if (qNode.distanceFromCamera < rightUpLOD)
                {
                    if (CameraFrustrum.Intersects(qNode.rightUpNode.boundBox))
                    {
                        qNode.status = 2;
                        RecursiveChildStatus(qNode.rightUpNode, cameraPoint, LODLevel, CameraFrustrum);
                    }
                    else
                    {
                        qNode.rightUpNode.inView = false;
                        qNode.status = 2;
                    }
                }
                if (qNode.distanceFromCamera < rightDownLOD)
                {
                    if (CameraFrustrum.Intersects(qNode.rightDownNode.boundBox))
                    {
                        qNode.status = 2;
                        RecursiveChildStatus(qNode.rightDownNode, cameraPoint, LODLevel, CameraFrustrum);
                    }
                    else
                    {
                        qNode.rightDownNode.inView = false;
                        qNode.status = 2;
                    }
                }
            }
        }

        /// <summary>
        /// This recursive function takes a point and sinks into the quadtree until it finds the active 
        /// Quadnode which currently covers that point.
        /// </summary>
        private QuadNode GetQuadNodeFromPoint(float xPos, float yPos, QuadNode rootNode)
        {
            if (rootNode.status != 1 && rootNode.NodeDepth < this.maxNodeDepth)
            {
                if (xPos < rootNode.XPosition * this.Scale + rootNode.NodeScale * this.Scale / 2)
                {
                    if (yPos < rootNode.YPosition * this.Scale + rootNode.NodeScale * this.Scale / 2)
                    {
                        return GetQuadNodeFromPoint(xPos, yPos, rootNode.leftUpNode);
                    }
                    else
                    {
                        return GetQuadNodeFromPoint(xPos, yPos, rootNode.rightUpNode);
                    }
                }
                else
                {
                    if (yPos < rootNode.YPosition * this.Scale + rootNode.NodeScale * this.Scale / 2)
                    {
                        return GetQuadNodeFromPoint(xPos, yPos, rootNode.leftDownNode);
                    }
                    else
                    {
                        return GetQuadNodeFromPoint(xPos, yPos, rootNode.rightDownNode);
                    }
                }
            }
            else
            {
                return rootNode;
            }
        }

#if DEBUG
        /// <summary>
        /// This stopwatch records various debug values, and stores them in the elapsed variables.
        /// </summary>
        Stopwatch debugTimer;
        /// <summary>
        /// Returns the time it takes to clear all the QuadNodes status to 0.
        /// </summary>
        public long elapsedTicks1;
        /// <summary>
        /// Returns the time it takes to recurse through all the quadnodes, check them against the Frustrums and distance, and set their status.
        /// </summary>
        public long elapsedTicks2;
        /// <summary>
        /// Returns the time it takes to pick which stitching type to use on each QuadNode.
        /// </summary>
        public long elapsedTicks4;
        /// <summary>
        /// Returns the time it takes to sort the Quadnodes by distance and add their index arrays to the all indices array.
        /// </summary>
        public long elapsedTicks5;
        /// <summary>
        /// Returns the time it takes to perform the draw call.
        /// </summary>
        public long elapsedTicks6;
#endif


        /// <summary>
        /// Gets the height map.
        /// </summary>
        /// <returns></returns>
        public float[,] getHeightMap()
        {
            float[,] map = new float[terrainWidth * (int)Scale, terrainHeight * (int)Scale];
            for (int x = 0; x < terrainWidth * Scale; x++) 
            {
                for (int y = 0; y < terrainHeight * Scale; y++)
                {
                    map[x, y] = getHeight(x, y);
                }
            }
            return map;
        }

        /// <summary>
        /// The terrains update method. Used to generate the index array that will be used to render the terrain.
        /// </summary>
        /// <param name="CameraPoint">The position of the camera. The dynamic LOD will be centered around this.</param>
        /// <param name="LODLevel">The LOD Level to apply. 3.5 is the recommended minimum, because of stitching issues. Any less than 2 may cause an exception, and at the very least will generate artifacts.</param>
        internal void UpdateTerrain(Vector3 CameraPoint, BoundingFrustum CameraFrustrum, float LODLevel)
        {
#if DEBUG
            debugTimer.Start();
#endif
            //////////////////////////////////////////////////////////////////////
            //Status every node as 0, to prepare for updating.
            foreach (QuadNode q in allQuadNodes)
            {
                q.status = 0;
                q.inView = true;
            }
#if DEBUG
            debugTimer.Stop();
            elapsedTicks1 = debugTimer.ElapsedTicks;
            debugTimer.Reset();

            debugTimer.Start();
#endif
            //////////////////////////////////////////////////////////////////////
            //Status the entire QuadTree, from the root node down
            RecursiveChildStatus(RootNode, CameraPoint, LODLevel, CameraFrustrum);
#if DEBUG
            debugTimer.Stop();

            elapsedTicks2 = debugTimer.ElapsedTicks;
            debugTimer.Reset();


            debugTimer.Start();
#endif
            //////////////////////////////////////////////////////////////////////
            //Perform Stitching
            #region Stitching Helper Chart
            //Index arrays for stitching
            //indices[0] = none

            //indices[1] = LEFT
            //indices[2] = TOP
            //indices[3] = RIGHT
            //indices[4] = DOWN

            //indices[5] = LEFT-TOP
            //indices[6] = TOP-RIGHT 
            //indices[7] = RIGHT-DOWN
            //indices[8] = DOWN-LEFT
            #endregion

            #region Apply stitching

            /////////////////////////////////////////////////
            //Applies Stitching to each QuadNode. 
            //An overly long and messy process, even though it isn't that complex.

            for (int i = 0; i < renderedindices.Length; i++)
            {
                renderedindices[i] = 0;
            }

            foreach (QuadNode q in allQuadNodes)
            {

                //if (q.status == 1)
                //{
                //    if (!CameraFrustrum.Intersects(q.boundBox))
                //    {
                //         q.status = 0;
                //    }
                //}



                if (q.status == 1)
                {
                    renderedindices[q.VBuffer] += q.numberOfIndices;
                    if (q.adjacentNorthQuad != null && q.adjacentSouthQuad != null && q.adjacentEastQuad != null && q.adjacentWestQuad != null)
                    {
                        if (q.adjacentNorthQuad.status < 1 && q.adjacentNorthQuad.inView)
                        {
                            //NORTHEAST
                            if (q.adjacentEastQuad.status < 1 && q.adjacentEastQuad.inView)
                            {
                                q.stitchedSides = 5;
                            }
                            else
                            {
                                //NORTHWEST
                                if (q.adjacentWestQuad.status < 1 && q.adjacentWestQuad.inView)
                                {
                                    q.stitchedSides = 6;
                                }
                                else
                                {
                                    //NORTH
                                    q.stitchedSides = 2;
                                }
                            }
                        }
                        else
                        {
                            if (q.adjacentSouthQuad.status < 1 && q.adjacentSouthQuad.inView)
                            {
                                //SOUTHEAST
                                if (q.adjacentEastQuad.status < 1 && q.adjacentEastQuad.inView)
                                {
                                    q.stitchedSides = 8;
                                }
                                else
                                {
                                    //SOUTHWEST
                                    if (q.adjacentWestQuad.status < 1 && q.adjacentWestQuad.inView)
                                    {
                                        q.stitchedSides = 7;
                                    }
                                    else
                                    {
                                        //SOUTH
                                        q.stitchedSides = 4;
                                    }
                                }
                            }
                            else
                            {
                                //EAST
                                if (q.adjacentEastQuad.status < 1 && q.adjacentEastQuad.inView)
                                {
                                    q.stitchedSides = 1;
                                }
                                else
                                {
                                    //WEST
                                    if (q.adjacentWestQuad.status < 1 && q.adjacentWestQuad.inView)
                                    {
                                        q.stitchedSides = 3;
                                    }
                                    else
                                    {
                                        //NO STITCHING
                                        q.stitchedSides = 0;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //////////////////////////////////////
                        //Map Edge Nodes
                        if (q.adjacentNorthQuad == null)
                        {
                            if (q.adjacentEastQuad == null)
                            {
                                if (q.adjacentSouthQuad == null)
                                {
                                    if (q.adjacentWestQuad == null)
                                    {
                                        q.stitchedSides = 0;
                                    }
                                }
                                else
                                {
                                    if (q.adjacentWestQuad != null)
                                    {
                                        //North East Corner
                                        if (q.adjacentSouthQuad.status < 1)
                                        {
                                            if (q.adjacentWestQuad.status < 1)
                                            {
                                                //SouthWest
                                                q.stitchedSides = 7;
                                            }
                                            else
                                            {
                                                //South
                                                q.stitchedSides = 4;
                                            }
                                        }
                                        else
                                        {
                                            if (q.adjacentWestQuad.status < 1)
                                            {
                                                //West
                                                q.stitchedSides = 3;
                                            }
                                            else
                                            {
                                                //No Stitching
                                                q.stitchedSides = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (q.adjacentWestQuad == null)
                                {
                                    //North West Corner
                                    if (q.adjacentSouthQuad.status < 1)
                                    {
                                        if (q.adjacentEastQuad.status < 1)
                                        {
                                            //SouthEast
                                            q.stitchedSides = 8;
                                        }
                                        else
                                        {
                                            //South
                                            q.stitchedSides = 4;
                                        }
                                    }
                                    else
                                    {
                                        if (q.adjacentEastQuad.status < 1)
                                        {
                                            //East
                                            q.stitchedSides = 1;
                                        }
                                        else
                                        {
                                            //None
                                            q.stitchedSides = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    //North Edge
                                    if (q.adjacentSouthQuad.status < 1)
                                    {
                                        if (q.adjacentEastQuad.status < 1)
                                        {
                                            //SouthEast
                                            q.stitchedSides = 8;
                                        }
                                        else
                                        {
                                            if (q.adjacentWestQuad.status < 1)
                                            {
                                                //SouthWest
                                                q.stitchedSides = 7;
                                            }
                                            else
                                            {
                                                //South
                                                q.stitchedSides = 4;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (q.adjacentEastQuad.status < 1)
                                        {
                                            //East
                                            q.stitchedSides = 1;
                                        }
                                        else
                                        {
                                            if (q.adjacentWestQuad.status < 1)
                                            {
                                                //West
                                                q.stitchedSides = 3;
                                            }
                                            else
                                            {
                                                //No Stitching
                                                q.stitchedSides = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //////////////////////////////////////////////
                            //If Not Part of North edge
                            //
                            if (q.adjacentSouthQuad == null)
                            {
                                if (q.adjacentEastQuad == null)
                                {
                                    //South East Corner
                                    if (q.adjacentNorthQuad.status < 1)
                                    {
                                        if (q.adjacentWestQuad.status < 1)
                                        {
                                            //northWest
                                            q.stitchedSides = 6;
                                        }
                                        else
                                        {
                                            //north
                                            q.stitchedSides = 2;
                                        }
                                    }
                                    else
                                    {
                                        if (q.adjacentWestQuad.status < 1)
                                        {
                                            //West
                                            q.stitchedSides = 3;
                                        }
                                        else
                                        {
                                            //No Stitching
                                            q.stitchedSides = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    if (q.adjacentWestQuad == null)
                                    {
                                        //South West Corner
                                        if (q.adjacentNorthQuad.status < 1)
                                        {
                                            if (q.adjacentEastQuad.status < 1)
                                            {
                                                //northEast
                                                q.stitchedSides = 5;
                                            }
                                            else
                                            {
                                                //north
                                                q.stitchedSides = 2;
                                            }
                                        }
                                        else
                                        {
                                            if (q.adjacentEastQuad.status < 1)
                                            {
                                                //East
                                                q.stitchedSides = 1;
                                            }
                                            else
                                            {
                                                //None
                                                q.stitchedSides = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //South Map Edge
                                        if (q.adjacentNorthQuad.status < 1)
                                        {
                                            if (q.adjacentEastQuad.status < 1)
                                            {
                                                //northEast
                                                q.stitchedSides = 5;
                                            }
                                            else
                                            {
                                                if (q.adjacentWestQuad.status < 1)
                                                {
                                                    //northWest
                                                    q.stitchedSides = 6;
                                                }
                                                else
                                                {
                                                    //north
                                                    q.stitchedSides = 2;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (q.adjacentEastQuad.status < 1)
                                            {
                                                //East
                                                q.stitchedSides = 1;
                                            }
                                            else
                                            {
                                                if (q.adjacentWestQuad.status < 1)
                                                {
                                                    //West
                                                    q.stitchedSides = 3;
                                                }
                                                else
                                                {
                                                    //No Stitching
                                                    q.stitchedSides = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //If not part of North or South edge:
                                if (q.adjacentEastQuad == null)
                                {
                                    //East Map Edge
                                    if (q.adjacentWestQuad.status < 1)
                                    {
                                        if (q.adjacentNorthQuad.status < 1)
                                        {
                                            //NorthWest
                                            q.stitchedSides = 6;
                                        }
                                        else
                                        {
                                            if (q.adjacentSouthQuad.status < 1)
                                            {
                                                //SouthWest
                                                q.stitchedSides = 7;
                                            }
                                            else
                                            {
                                                //West
                                                q.stitchedSides = 3;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (q.adjacentNorthQuad.status < 1)
                                        {
                                            //North
                                            q.stitchedSides = 2;
                                        }
                                        else
                                        {
                                            if (q.adjacentSouthQuad.status < 1)
                                            {
                                                //South
                                                q.stitchedSides = 4;
                                            }
                                            else
                                            {
                                                //No Stitching
                                                q.stitchedSides = 0;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //If not east, north or south.
                                    if (q.adjacentWestQuad == null)
                                    {
                                        //West Map Edge
                                        if (q.adjacentEastQuad.status < 1)
                                        {
                                            if (q.adjacentNorthQuad.status < 1)
                                            {
                                                //NorthEast
                                                q.stitchedSides = 5;
                                            }
                                            else
                                            {
                                                if (q.adjacentSouthQuad.status < 1)
                                                {
                                                    //SouthEast
                                                    q.stitchedSides = 8;
                                                }
                                                else
                                                {
                                                    //East
                                                    q.stitchedSides = 1;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (q.adjacentNorthQuad.status < 1)
                                            {
                                                //North
                                                q.stitchedSides = 2;
                                            }
                                            else
                                            {
                                                if (q.adjacentSouthQuad.status < 1)
                                                {
                                                    //South
                                                    q.stitchedSides = 4;
                                                }
                                                else
                                                {
                                                    //No Stitching
                                                    q.stitchedSides = 0;
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
#if DEBUG
            debugTimer.Stop();
            elapsedTicks4 = debugTimer.ElapsedTicks;
            debugTimer.Reset();



            debugTimer.Start();
#endif

            /////////////////////////////////////////////////
            //Begin compiling the main Index array's
            {
                int i = 0;
                #region depricated
                //for (i = 0; i < allIndices.Length; i++)
                //{
                //    if (renderedindices[i] > 0)
                //    {
                //    //    allIndices[i] = new int[renderedindices[i]];
                //     //   Array.Clear(allIndices[i], 0, allIndices[i].Length);
                //    }
                //    else
                //    {
                //     //   Array.Clear(allIndices[i], 0, allIndices[i].Length);
                //     //   allIndices[i] = new int[3];
                //        //Array.Clear(allIndices[i], 0, allIndices[i].Length);
                //    }
                //}
                #endregion

                for (i = 0; i < renderedindices.Length; i++)
                {
                    renderedindices[i] = 0;
                }


                qnl.Clear();

                for (i = 0; i < allQuadNodes.Count; i++)
                {
                    //Select only the leaf nodes
                    if (allQuadNodes[i].status == 1)
                    {
                        ////////////////////////////
                        //Add selected nodes to sorting list
                        qnl.Add(allQuadNodes[i]);
                    }
                }
                //Sort LeafNodes by distance to save fillrate
                qnl.Sort(compareQuadNodesByDistance);

                foreach (QuadNode q in qnl)
                {
                    //Compile sorted leaf nodes into array
                    q.indices[q.stitchedSides].CopyTo(allIndices[q.VBuffer], renderedindices[q.VBuffer]);
                    //Add to indice count     
                    renderedindices[q.VBuffer] += q.numberOfIndices;
                }
            }
#if DEBUG
            debugTimer.Stop();
            elapsedTicks5 = debugTimer.ElapsedTicks;
            debugTimer.Reset();
#endif


            //////////////////////////////////////////////////////////////////////
            //Add the main index arrays to the various Index Buffers.
            if (first != true)
            {
                for (int k = 0; k < allIBs.Length; k++)
                {
                    if (renderedindices[k] > 0)
                    {
                        allIBs[k].SetData<int>(0, allIndices[k], 0, renderedindices[k], SetDataOptions.Discard);
                    }
                }
            }
            first = false;
        }

        bool first;
        //This list is used for sorting: all leaf quadnodes are added to it before they are sorted by distance.
        List<QuadNode> qnl;
        /// <summary>
        /// This simple function compares two quadnode by distance. Used for sorting to save overdraw.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int compareQuadNodesByDistance(QuadNode x, QuadNode y)
        {
            int retval = x.distanceFromCamera.CompareTo(y.distanceFromCamera);

            if (retval < 0)
            {
                return -1;
            }
            else
            {
                if (retval > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        
        ///////////////////////////////////////////////////////
        /// <summary>
        /// Draws the terrain. Uses the Terrain Shader to draw the indices which should have been compiled and sorted during the update call.
        /// </summary>
        /// <param name="TerrainShader">The terrain shader.</param>
        /// <param name="device">Current graphics device to draw to.</param>
        internal void DrawTerrain(Effect TerrainShader, GraphicsDevice device)
        {

#if DEBUG
            debugTimer.Start();
#endif           
            
            /////////////////////////////////////////////////
            //Draw the terrain for the compiled index and vertex buffers.
            for (int i = 0; i < allVertices.Length; i++)
            {
                
                device.Indices = allIBs[i];
                device.SetVertexBuffer(allVBs[i]);
                if (renderedindices[i] > 0)
                {
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, allVertices[i].Length, 0, renderedindices[i] / 3);
                }
                device.Indices = null;
            }

#if DEBUG
            debugTimer.Stop();
            elapsedTicks6 = debugTimer.ElapsedTicks;
            debugTimer.Reset();
#endif
        }
        #region getHeightFast
        /// <summary>
        /// A quick method for getting the height of the terrain at the current postion.  Not as accurate as getHeight, but slightly faster. 
        /// It's not really worth using this function: the difference is tiny.
        /// </summary>
        /// <param name="xPos">The x position to get the terrain height at</param>
        /// <param name="zPos">The y position to get the terrain height at</param>
        /// <returns>A float indicating the height of the terrain at the requested position</returns>
        public float getHeightFast(float xPos, float zPos)
        {
            int left, top;
            left = (int)xPos / (int)Scale;
            top = (int)zPos / (int)Scale;

            float cZ;

            if (top > 0 && left > 0 && top < terrainHeight - 1 && left < terrainWidth - 1)
            {
                float xNormalized = (xPos % Scale) / Scale;
                float zNormalized = (zPos % Scale) / Scale;

                float topHeight = MathHelper.Lerp(
                  heightStore[left, top],
                  heightStore[left + 1, top], xNormalized);

                float bottomHeight = MathHelper.Lerp(
                    heightStore[left, top + 1],
                    heightStore[left + 1, top + 1], xNormalized);

                cZ = MathHelper.Lerp(topHeight, bottomHeight, zNormalized);
            }
            else
            {
                cZ = 0;
            }
            cZ *= HeightScale;

            return cZ;

        }
        #endregion
        #region getHeight
        /// <summary>
        /// An accurate method for getting the height of the terrain at the current postion. Completely accurate to the geometry.
        /// </summary>
        /// <param name="xPos">The x position to get the terrain height at</param>
        /// <param name="zPos">The y position to get the terrain height at</param>
        /// <returns>A float indicating the height of the terrain at the requested position</returns>
        public float getHeight(float xPos, float zPos)
        {
            int left, top;
            left = (int)xPos / (int)Scale;
            top = (int)zPos / (int)Scale;

            float cZ;


            if (top > 0 && left > 0 && top < terrainHeight - Scale && left < terrainWidth - Scale)
            {
                float rightDiagDifference = heightStore[left, top] - heightStore[left + 1, top + 1];
                float leftDiagDifference = heightStore[left + 1, top] - heightStore[left, top + 1];



                if (Math.Abs(rightDiagDifference) >= Math.Abs(leftDiagDifference))
                {
                    if (-((xPos % Scale) / Scale) + 1 > (zPos % Scale) / Scale)
                    {
                        float xNormalized = (xPos % Scale) / Scale;
                        float zNormalized = (zPos % Scale) / Scale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top],
                          heightStore[left + 1, top],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left, top + 1],
                          heightStore[left, top + 1] - (heightStore[left, top] - heightStore[left + 1, top]),
                          xNormalized);

                        cZ = MathHelper.Lerp(topHeight, bottomHeight, zNormalized);
                    }
                    else
                    {
                        float xNormalized = (xPos % Scale) / Scale;
                        float zNormalized = (zPos % Scale) / Scale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top + 1],
                          heightStore[left + 1, top + 1],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left + 1, top] + (heightStore[left, top + 1] - heightStore[left + 1, top + 1]),
                          heightStore[left + 1, top],
                          xNormalized);

                        cZ = MathHelper.Lerp(bottomHeight, topHeight, zNormalized);
                    }
                }
                else
                {
                    if (((xPos % Scale) / Scale) > (zPos % Scale) / Scale)
                    {

                        float xNormalized = (xPos % Scale) / Scale;
                        float zNormalized = (zPos % Scale) / Scale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top],
                          heightStore[left + 1, top],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left + 1, top + 1] + (heightStore[left, top] - heightStore[left + 1, top]),
                          heightStore[left + 1, top + 1],
                          xNormalized);

                        cZ = MathHelper.Lerp(topHeight, bottomHeight, zNormalized);
                    }
                    else
                    {
                        float xNormalized = (xPos % Scale) / Scale;
                        float zNormalized = (zPos % Scale) / Scale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top + 1],
                          heightStore[left + 1, top + 1],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left, top],
                          heightStore[left, top] - (heightStore[left, top + 1] - heightStore[left + 1, top + 1]),
                          xNormalized);

                        cZ = MathHelper.Lerp(bottomHeight, topHeight, zNormalized);
                    }
                }
            }
            else
            {
                cZ = 0;
            }
            cZ *= HeightScale;

            return cZ;
        }
        #endregion
        #region getHeightLOD
        /// <summary>
        /// Gets the height of the terrain, taking into account the level of detail at the position.
        /// </summary>
        /// <param name="xPos">The x position to get the terrain height at</param>
        /// <param name="zPos">The y position to get the terrain height at</param>
        /// <returns>A float indicating the height of the terrain at the requested position</returns>
        public float getLODHeight(float xPos, float zPos)
        {
            QuadNode currentNode = GetQuadNodeFromPoint(xPos, zPos, RootNode);

            float squareScale = Scale * currentNode.stepSize;
            int stepSize = currentNode.stepSize;

            int left, top;
            left = ((int)xPos / (int)squareScale) * stepSize;
            top = ((int)zPos / (int)squareScale) * stepSize;

            float cZ;


            if (top > 0 && left > 0 && top < terrainHeight - Scale && left < terrainWidth - Scale)
            {
                float rightDiagDifference = heightStore[left, top] - heightStore[left + stepSize, top + stepSize];
                float leftDiagDifference = heightStore[left + stepSize, top] - heightStore[left, top + stepSize];



                if (Math.Abs(rightDiagDifference) >= Math.Abs(leftDiagDifference))
                {
                    if (-((xPos % squareScale) / squareScale) + 1 > (zPos % squareScale) / squareScale)
                    {
                        float xNormalized = (xPos % squareScale) / squareScale;
                        float zNormalized = (zPos % squareScale) / squareScale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top],
                          heightStore[left + stepSize, top],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left, top + stepSize],
                          heightStore[left, top + stepSize] - (heightStore[left, top] - heightStore[left + stepSize, top]),
                          xNormalized);

                        cZ = MathHelper.Lerp(topHeight, bottomHeight, zNormalized);
                    }
                    else
                    {
                        float xNormalized = (xPos % squareScale) / squareScale;
                        float zNormalized = (zPos % squareScale) / squareScale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top + stepSize],
                          heightStore[left + stepSize, top + stepSize],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left + stepSize, top] + (heightStore[left, top + stepSize] - heightStore[left + stepSize, top + stepSize]),
                          heightStore[left + stepSize, top],
                          xNormalized);

                        cZ = MathHelper.Lerp(bottomHeight, topHeight, zNormalized);
                    }
                }
                else
                {
                    if (((xPos % squareScale) / squareScale) > (zPos % squareScale) / squareScale)
                    {

                        float xNormalized = (xPos % squareScale) / squareScale;
                        float zNormalized = (zPos % squareScale) / squareScale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top],
                          heightStore[left + stepSize, top],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left + stepSize, top + stepSize] + (heightStore[left, top] - heightStore[left + stepSize, top]),
                          heightStore[left + stepSize, top + stepSize],
                          xNormalized);

                        cZ = MathHelper.Lerp(topHeight, bottomHeight, zNormalized);
                    }
                    else
                    {
                        float xNormalized = (xPos % squareScale) / squareScale;
                        float zNormalized = (zPos % squareScale) / squareScale;

                        float topHeight = MathHelper.Lerp(
                          heightStore[left, top + stepSize],
                          heightStore[left + stepSize, top + stepSize],
                          xNormalized);

                        float bottomHeight = MathHelper.Lerp(
                          heightStore[left, top],
                          heightStore[left, top] - (heightStore[left, top + stepSize] - heightStore[left + stepSize, top + stepSize]),
                          xNormalized);

                        cZ = MathHelper.Lerp(bottomHeight, topHeight, zNormalized);
                    }
                }
            }
            else
            {
                cZ = 0;
            }
            cZ *= HeightScale;

            return cZ;
        }
        #endregion

        #region getNormalSmooth
        /// <summary>
        /// A quick method for getting the normal of the terrain at the current postion. Returns a smooth, but less accurate, value than GetNormalSharp.
        /// Good for high detail terrains.
        /// </summary>
        /// <param name="xPos">The x position to get the terrain normal at</param>
        /// <param name="zPos">The y position to get the terrain normal at</param>
        /// <returns>A Vector3 indicating the normal of the terrain at the requested position</returns>
        public Vector3 getNormalSmooth(float xPos, float zPos)
        {
            int left, top;
            left = (int)xPos / (int)Scale;
            top = (int)zPos / (int)Scale;

            Vector3 cZ;

            if (top > 0 && left > 0 && top < terrainHeight - Scale && left < terrainWidth - Scale)
            {
                float xNormalized = (xPos % Scale) / Scale;
                float zNormalized = (zPos % Scale) / Scale;

                Vector3 topNorm;
                Vector3.Lerp(
                  ref normStore[left, top],
                  ref normStore[left + 1, top],
                  xNormalized,
                  out topNorm);

                Vector3 bottomNorm;
                Vector3.Lerp(
                    ref normStore[left, top + 1],
                    ref normStore[left + 1, top + 1],
                    xNormalized,
                    out bottomNorm);

                Vector3.Lerp(ref topNorm, ref bottomNorm, zNormalized, out cZ);
            }
            else
            {
                cZ = Vector3.Up;
            }

            return cZ;

        }
        #endregion
        #region getNormalSharp
        /// <summary>
        /// An accurate method for getting the normal of the terrain at the current postion. Returns the exact normal of the triangle.
        /// </summary>
        /// <param name="xPos">The x position to get the terrain height at</param>
        /// <param name="zPos">The y position to get the terrain height at</param>
        /// <returns>A float indicating the height of the terrain at the requested position</returns>
        public Vector3 getNormal(float xPos, float zPos)
        {
            int left, top;
            left = (int)xPos / (int)Scale;
            top = (int)zPos / (int)Scale;

            Vector3 cZ;


            if (top > 0 && left > 0 && top < terrainHeight - Scale && left < terrainWidth - Scale)
            {
                float rightDiagDifference = heightStore[left, top] - heightStore[left + 1, top + 1];
                float leftDiagDifference = heightStore[left + 1, top] - heightStore[left, top + 1];



                if (Math.Abs(rightDiagDifference) >= Math.Abs(leftDiagDifference))
                {
                    if (-((xPos % Scale) / Scale) + 1 > (zPos % Scale) / Scale)
                    {
                        //  float xNormalized = (xPos % Scale) / Scale;
                        //  float zNormalized = (zPos % Scale) / Scale;

                        //  Vector3 topNorm;
                        //  Vector3.Lerp(
                        //    ref normStore[left, top],
                        //    ref normStore[left + 1, top],
                        //    xNormalized,
                        //    out topNorm);

                        //  Vector3 bottomNorm = Vector3.Lerp(
                        //normStore[left, top + 1],
                        //normStore[left, top + 1] - (normStore[left, top] - normStore[left + 1, top]),
                        //xNormalized);

                        //  Vector3.Lerp(ref topNorm, ref bottomNorm, zNormalized, out cZ);

                        cZ = Vector3.Cross(normStore[left, top] - normStore[left + 1, top], normStore[left, top] - normStore[left, top + 1]);
                    }
                    else
                    {
                        //float xNormalized = (xPos % Scale) / Scale;
                        //float zNormalized = (zPos % Scale) / Scale;


                        //Vector3 topNorm;
                        //Vector3.Lerp(
                        // ref normStore[left, top + 1],
                        // ref normStore[left + 1, top + 1],
                        //  xNormalized,
                        //  out topNorm);

                        //Vector3 bottomNorm = Vector3.Lerp(
                        //  normStore[left + 1, top] + (normStore[left, top + 1] - normStore[left + 1, top + 1]),
                        //  normStore[left + 1, top],
                        //  xNormalized);

                        //Vector3.Lerp(ref bottomNorm, ref topNorm, zNormalized, out cZ);
                        cZ = Vector3.Cross(normStore[left + 1, top + 1] - normStore[left, top + 1], normStore[left + 1, top + 1] - normStore[left + 1, top]);


                    }
                }
                else
                {
                    if (((xPos % Scale) / Scale) > (zPos % Scale) / Scale)
                    {

                        //float xNormalized = (xPos % Scale) / Scale;
                        //float zNormalized = (zPos % Scale) / Scale;

                        //Vector3 topNorm;
                        //Vector3.Lerp(
                        //  ref normStore[left, top],
                        //  ref normStore[left + 1, top],
                        //  xNormalized,
                        //  out topNorm);

                        //Vector3 bottomNorm = Vector3.Lerp(
                        // normStore[left + 1, top + 1] + (normStore[left, top] - normStore[left + 1, top]),
                        // normStore[left + 1, top + 1],
                        //  xNormalized);

                        //Vector3.Lerp(ref topNorm, ref bottomNorm, zNormalized, out cZ);
                        cZ = Vector3.Cross(normStore[left + 1, top] - normStore[left, top], normStore[left + 1, top] - normStore[left + 1, top + 1]);
                    }
                    else
                    {
                        //float xNormalized = (xPos % Scale) / Scale;
                        //float zNormalized = (zPos % Scale) / Scale;

                        //Vector3 topNorm;
                        //Vector3.Lerp(
                        // ref normStore[left, top + 1],
                        // ref normStore[left + 1, top + 1],
                        //  xNormalized,
                        //  out topNorm);

                        //Vector3 bottomNorm = Vector3.Lerp(
                        //  normStore[left, top],
                        //  normStore[left, top] - (normStore[left, top + 1] - normStore[left + 1, top + 1]),
                        //  xNormalized);

                        //Vector3.Lerp(ref bottomNorm, ref topNorm, zNormalized, out cZ);
                        cZ = Vector3.Cross(normStore[left, top + 1] - normStore[left + 1, top + 1], normStore[left, top + 1] - normStore[left, top]);

                    }
                }
            }
            else
            {
                cZ = Vector3.Up;
            }
            cZ *= HeightScale;

            return cZ;
        }
        #endregion
        ////////////////////////////////////////////
        ///GET NORMAL FUNCTION &
        ///GET NORMAL LOD FUNCTION...
        ////////////////////////////////////////////
    }




    /// <summary>
    /// Class containing all data for a single node in a QuadTree, as
    /// well as a constructer to build the node
    /// </summary>
    public class QuadNode
    {
        //The children nodes
        public QuadNode leftUpNode;
        public QuadNode rightUpNode;
        public QuadNode leftDownNode;
        public QuadNode rightDownNode;

        //The parent node
        public QuadNode parent;

        //The nodes of equal size which are adjacent to this node
        public QuadNode adjacentNorthQuad;
        public QuadNode adjacentSouthQuad;
        public QuadNode adjacentEastQuad;
        public QuadNode adjacentWestQuad;

        //Determines which of the stiching versions of this node to display.
        public int stitchedSides;

        //X&Y origins of this node on the heightmap
        public int XPosition;
        public int YPosition;

        //Node depth: 0=Root Node
        public int NodeDepth;
        //Some other things related to NodeDepth
        public int NodeLevel;
        public int NodeScale;

        //A jagged array to contain all the index buffers for this node.
        public int[][] indices;

        //The current status of this node in the tree.
        public short status; //0=BelowLeaf :: 1=LeafNode :: 2=AboveLeaf

        //This nodes bounding box (for view frustrums)
        public BoundingBox boundBox;
        public bool inView;

        public float distanceFromCamera;

        //Related to Multiple vertex Buffers
        public int VBuffer;
        int VBXOffset;
        int VBYOffset;

        public int stepSize;

        //Added to rendered indices when this is a leaf node.
        public int numberOfIndices;


        //heightMap must be 2^n+1, squareSize must be 2^n+1, both are constants of the Quadtree.
        //nodedepth will not go below a level that will make square size impossible given the height field.
        /// <summary>
        /// This constructs a single complete quadNode in the Quadtree.
        /// </summary>
        /// <param name="heightMap">This should be a reference to the Terrains heightmap. Inherited from Quadterrain.</param>
        /// <param name="squareSize">This is the number of vertices along the edge of the quadnode. Inherited from Quadterrain.</param>
        /// <param name="VertBuffSize">This is the number of vertices along the edge of a Vertex buffer. Inherited from Quadterrain.</param>
        /// <param name="terrainScale">The scale of the terrain. Inherited from Quadterrain.</param>
        /// <param name="nodeDepth">The depth of this individual node. Vitally important for generating many properties of the node.</param>
        /// <param name="xPosition">The X origin of the node on the heightmap.</param>
        /// <param name="yPosition">The Y origin of the node on the heightmap.</param>
        /// <param name="parentFullQuad">The parent node of this node in the Tree.</param>
        /// <param name="Identifier">An ID number. I can't actually remember what I used it for... :?</param>
        /// <param name="Vertices">This should be a reference to the Vertex Buffers. Inherited from Quadterrain.</param>
        public QuadNode(Texture2D heightMap, Vector3[,] normStore, int squareSize, int VertBuffSize, float terrainScale, int nodeDepth, int xPosition, int yPosition, QuadNode parentFullQuad, VertexPosition[][] Vertices)
        {

            stitchedSides = 0;

            //Initialise x and y position variables.
            XPosition = xPosition;
            YPosition = yPosition;
            //Initialise parent variable
            parent = parentFullQuad;
            //Initialise status
            status = 0;

            //Get number of Vertex Buffers
            int numberOfVBs = (int)Math.Pow((heightMap.Height - 1) / (VertBuffSize - 1), 2);
            int sqrtNumberOfVBs = (int)Math.Sqrt(numberOfVBs);

            /////////////////////////////////////////////////////
            // For:
            // SquareSize = 5
            // Height = 257
            // NodeDepth,   NodeLevel,  NodeScale,  stepSize
            // 0,           1,          257,        64
            // 1,           2,          129,        32
            // 2,           4,          65,         16
            // 3,           8,          33,         8
            // 4,           16,         19,         4
            // 5,           32,         9,          2
            // 6,           64,         5,          1
            ////////////////////////////////////////////////////

            int stitching = 0;


            NodeLevel = (int)Math.Pow(2, (nodeDepth));
            NodeScale = ((heightMap.Height - 1) / NodeLevel) + 1;
            stepSize = (NodeScale - 1) / (squareSize - 1);


            indices = new int[9][];


            ////////////////////////////////////////////////////
            //Gather the vertex buffer of this node
            VBuffer = (int)(Math.Floor(((float)(xPosition + 1) / heightMap.Width) * sqrtNumberOfVBs) + (Math.Floor(((float)(yPosition + 1) / heightMap.Height) * sqrtNumberOfVBs) * sqrtNumberOfVBs)) + 1;
            int VBufferRootScale = 1;

            //Set this nodes buffer to the root buffer if the Node is too big
            if (NodeScale > VertBuffSize)
            {
                VBuffer = 0;
                VBufferRootScale = (heightMap.Width - 1) / (VertBuffSize - 1); // ~~ HeightMapWidth/VBufferSize ~~
                VBXOffset = 0;
                VBYOffset = 0;
            }
            else
            {
                VBXOffset = ((VBuffer - 1) % (int)sqrtNumberOfVBs) * (VertBuffSize - 1);
                VBYOffset = ((VBuffer - 1) / (int)sqrtNumberOfVBs) * (VertBuffSize - 1);
            }

            //Initialised Bounding Box variables
            float minHeight = 10000;
            float maxHeight = 0;


            //indices[0] = none

            //indices[1] = LEFT
            //indices[2] = TOP
            //indices[3] = RIGHT
            //indices[4] = DOWN

            //indices[5] = LEFT-TOP
            //indices[6] = TOP-RIGHT
            //indices[7] = RIGHT-DOWN
            //indices[8] = DOWN-LEFT

            //Generate Index array for this node.
            for (stitching = 0; stitching < 9; stitching++)
            {
                List<int> tempIndices = new List<int>();

                //These variables are used for creating flatstrips
                bool addingFlats = false;
                int xOffset = 0;
                Vector3 oldNormal = new Vector3();

                //Build Index arrays for each stitching type
                for (int y = yPosition; y < yPosition + ((NodeScale - 1) - stepSize + 1); y += stepSize)
                {
                    for (int x = xPosition; x < xPosition + ((NodeScale - 1) - stepSize + 1); x += stepSize)
                    {
                        // Test
                        if (y < heightMap.Width - stepSize && x < heightMap.Height - stepSize)
                        {
                            //Edge stitching:
                            //
                            //
                            //1 Lrge stitch
                            //     /\
                            //    /__\
                            //
                            //
                            //2 Sml stitches
                            //  ___  ___
                            //  | /  \ |
                            //  |/    \|
                            bool standard = false;

                            if (Vertices[VBuffer][Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale].Position.Y > maxHeight)
                            {
                                maxHeight = Vertices[VBuffer][Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale].Position.Y;
                            }
                            else
                            {
                                if (Vertices[VBuffer][Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale].Position.Y < minHeight)
                                {
                                    minHeight = Vertices[VBuffer][Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale].Position.Y;
                                }
                            }

                            /////////////////////////////////////////////////////
                            //STITCH BOTTOM
                            if (x <= xPosition)
                            {
                                if (stitching == 4 || stitching == 7 || stitching == 8)
                                {
                                    if (y % (stepSize * 2) == 0)
                                    {
                                        standard = false;

                                        //1 Lrge stitch
                                        tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize * 2) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                        numberOfIndices += 3;

                                        //2 Sml stitches (Don't do edges)
                                        if (y != yPosition || stitching == 8 || stitching == 4)
                                        {
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                        }
                                        if (y != yPosition + (NodeScale - 1) - stepSize * 2 || stitching == 7 || stitching == 4)
                                        {
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize * 2) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize * 2) * VertBuffSize) / VBufferRootScale);
                                        }
                                    }
                                }
                                else
                                {
                                    standard = true;
                                }
                            }



                            //////////////////////////////////////////
                            //STITCH TOP
                            if (x == xPosition + ((NodeScale - 1) - stepSize))
                            {
                                if (stitching == 2 || stitching == 5 || stitching == 6)
                                {
                                    if (y % (stepSize * 2) == 0)
                                    {
                                        standard = false;

                                        //1 Lrg Stich
                                        tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize * 2) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                        //2 Smll Stitches (don't do edges)
                                        if (y != yPosition || stitching == 2 || stitching == 5)
                                        {
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                        }
                                        if (y != yPosition + (NodeScale - 1) - stepSize * 2 || stitching == 2 || stitching == 6)
                                        {

                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize * 2) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset) + stepSize * 2) * VertBuffSize) / VBufferRootScale);
                                        }
                                    }
                                }
                                else
                                {
                                    standard = true;
                                }
                            }


                            ////////////////////////////////
                            //STITCH RIGHT
                            if (y <= yPosition)
                            {
                                if (stitching == 3 || stitching == 6 || stitching == 7)
                                {
                                    if (x % (stepSize * 2) == 0)
                                    {
                                        standard = false;

                                        tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize * 2) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                        if (x != xPosition || stitching == 3 || stitching == 6)
                                        {
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                        }
                                        if (x != xPosition + (NodeScale - 1) - stepSize * 2 || stitching == 3 || stitching == 7)
                                        {
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize * 2) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize * 2) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        }
                                    }
                                }
                                else
                                {
                                    standard = true;
                                }
                            }


                            //////////////////////////////////////////
                            //STITCH LEFT
                            if (y == yPosition + ((NodeScale - 1) - stepSize))
                            {
                                if (stitching == 1 || stitching == 5 || stitching == 8)
                                {
                                    if (x % (stepSize * 2) == 0)
                                    {
                                        standard = false;

                                        //1 Lrg Stich
                                        tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize * 2) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);

                                        //2 Smll Stitches (don't do edges)
                                        if (x != xPosition || stitching == 1 || stitching == 5)
                                        {
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        }
                                        if (x != xPosition + (NodeScale - 1) - stepSize * 2 || stitching == 1 || stitching == 8)
                                        {

                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize * 2) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize * 2) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale);
                                        }
                                    }
                                }
                                else
                                {
                                    standard = true;
                                }
                            }

                            if (x > xPosition && y > yPosition && x != xPosition + ((NodeScale - 1) - stepSize) && y != yPosition + ((NodeScale - 1) - stepSize))
                            {
                                standard = true;
                            }

                            //NO STITCHING (Centre piece)
                            if (standard == true)
                            {
                                #region depricated
                                //indices[stitching][i++] = Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale;
                                //indices[stitching][i++] = Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale;
                                //indices[stitching][i++] = Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale;

                                //indices[stitching][i++] = Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale;
                                //indices[stitching][i++] = Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale;
                                //indices[stitching][i++] = Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale;
                                #endregion

                                /////////////////////////////////////////////////////////////////////
                                //This section is used to cut the number of polygons by removing detail on flat planes
                                Vector3 next1Normal = normStore[x, y];
                                Vector3 next2Normal = normStore[x, y + stepSize];

                                Vector3 next1Position = Vertices[VBuffer][Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale].Position;
                                Vector3 next2Position = Vertices[VBuffer][Math.Abs((x - VBXOffset) + (y + stepSize - VBYOffset) * VertBuffSize) / VBufferRootScale].Position;

                                Vector3 nn = Vertices[VBuffer][Math.Abs((x + stepSize - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale].Position - next1Position;
                                nn.Normalize();

                                Vector3 nn2 = Vertices[VBuffer][Math.Abs((x + stepSize - VBXOffset) + (y + stepSize - VBYOffset) * VertBuffSize) / VBufferRootScale].Position - next2Position;
                                nn2.Normalize();

                                Vector3 next1Normalised = Vector3.Cross(next1Normal, Vector3.UnitZ);
                                next1Normalised.Normalize();

                                Vector3 next2Normalised = Vector3.Cross(next2Normal, Vector3.UnitZ);
                                next2Normalised.Normalize();
                                //MessageBox.Show("nn: "+ nn +", next2Position: " + next2Position.ToString() + ", next1Position: " + next1Position.ToString() + ",n1normalised: " + next1Normalised.ToString() + ", Formula: " + Vector3.Add(next1Position, Vector3.Multiply(next1Normalised, terrainScale * stepSize)));


                                //if (Math.Abs(oldNormal.X - next1Normal.X) < 0.000001 && Math.Abs(oldNormal.X - next2Normal.X) < 0.000001 && Math.Abs(oldNormal.Y - next1Normal.Y) < 0.000001 && Math.Abs(oldNormal.Y - next2Normal.Y) < 0.000001 && x < xPosition + ((NodeScale - 1) - stepSize * 2))
                                //if (oldNormal == next1Normal && oldNormal == next2Normal && Math.Abs((nn.Y) - (next1Normalised.Y)) < .00001 && Math.Abs((nn2.Y) - (next2Normalised.Y)) < .00001 && x < xPosition + ((NodeScale - 1) - stepSize * 2) || oldNormal == next1Normal && oldNormal == next2Normal && float.IsNaN(Math.Abs(Math.Abs(nn.Z) - Math.Abs(next1Normalised.Z))) && x < xPosition + ((NodeScale - 1) - stepSize * 2))
                                if (oldNormal == next1Normal && oldNormal == next2Normal && nn == next1Normalised && nn2 == next2Normalised && x < xPosition + ((NodeScale - 1) - stepSize * 2) || oldNormal == next1Normal && oldNormal == next2Normal && float.IsNaN((next1Normalised.Z)) && nn.X == 1 && nn2.X == 1 && x < xPosition + ((NodeScale - 1) - stepSize * 2))
                                {
                                    //MessageBox.Show(nn+" "+next1Normalised);
                                    if (addingFlats == false)
                                    {
                                        //Activates when we enter a flatstrip
                                        addingFlats = true;
                                        xOffset = x;
                                        oldNormal = normStore[x, y + stepSize];
                                    }
                                    else
                                    {
                                        //In the middle of a flatstip, we do nothing
                                    }
                                }
                                else
                                {
                                    if (addingFlats == true)
                                    {
                                        //Activaes when we leave a flatstrip
                                        addingFlats = false;
                                        tempIndices.Add(Math.Abs(((xOffset - VBXOffset)) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs((x - stepSize - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs((x - stepSize - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                        tempIndices.Add(Math.Abs(((xOffset - VBXOffset)) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs((x - stepSize - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((xOffset - VBXOffset)) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);


                                        /////////////////////////////////////////////////////////////////////
                                        //This makes cliffs look better by reorientating the diagonal, and thus removing "cliff feet".
                                        VertexPosition v0 = Vertices[VBuffer][Math.Abs((x - VBXOffset) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale];
                                        VertexPosition vX = Vertices[VBuffer][Math.Abs((x - VBXOffset + stepSize) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale];
                                        VertexPosition vY = Vertices[VBuffer][Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale];
                                        VertexPosition vXY = Vertices[VBuffer][Math.Abs((x - VBXOffset + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale];

                                        float rightDiagDifference = Math.Abs(v0.Position.Y - vXY.Position.Y);
                                        float leftDiagDifference = Math.Abs(vX.Position.Y - vY.Position.Y);


                                        if (rightDiagDifference < leftDiagDifference)
                                        {
                                            tempIndices.Add(Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);


                                            tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                        }
                                        else
                                        {
                                            tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                            tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                        }

                                        tempIndices.Add(Math.Abs((x - stepSize - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - stepSize - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs((x - stepSize - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                        tempIndices.Add(Math.Abs((x - stepSize - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - stepSize - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                        tempIndices.Add(Math.Abs(((x - stepSize - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                        oldNormal = normStore[x, y + stepSize];
                                        /////////////////////////////////////////////////////////////////////
                                    }
                                    else
                                    {
                                        /////////////////////////////////////////////////////////////////////
                                        //Generate a normal square: nothing special, except for removing cliff feet
                                        oldNormal = normStore[x, y + stepSize];

                                        //Make cliffs look better by reorientating the diagonal
                                        VertexPosition v0 = Vertices[VBuffer][Math.Abs((x - VBXOffset) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale];
                                        VertexPosition vX = Vertices[VBuffer][Math.Abs((x - VBXOffset + stepSize) + ((y - VBYOffset)) * VertBuffSize) / VBufferRootScale];
                                        VertexPosition vY = Vertices[VBuffer][Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale];
                                        VertexPosition vXY = Vertices[VBuffer][Math.Abs((x - VBXOffset + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale];

                                        float rightDiagDifference = Math.Abs(v0.Position.Y - vXY.Position.Y);
                                        float leftDiagDifference = Math.Abs(vX.Position.Y - vY.Position.Y);



                                        if (rightDiagDifference < leftDiagDifference)
                                        {
                                            tempIndices.Add(Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);


                                            tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset)) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                        }
                                        else
                                        {
                                            tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs((x - VBXOffset) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);

                                            tempIndices.Add(Math.Abs((x - VBXOffset) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + ((y - VBYOffset) + stepSize) * VertBuffSize) / VBufferRootScale);
                                            tempIndices.Add(Math.Abs(((x - VBXOffset) + stepSize) + (y - VBYOffset) * VertBuffSize) / VBufferRootScale);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                /////////////////////////////////////////////////////////////////////
                //Keep track of the number of indices.
                indices[stitching] = new int[tempIndices.Count];
                if (numberOfIndices < tempIndices.Count)
                    numberOfIndices = tempIndices.Count;
                indices[stitching] = tempIndices.ToArray();
            }


            ////////////////////////////////////////////////
            //Create Nodes Bounding Box.
            boundBox = new BoundingBox(new Vector3((float)(xPosition) * terrainScale, minHeight, (float)(yPosition) * terrainScale),
                                        new Vector3((float)(xPosition + NodeScale) * terrainScale, maxHeight, (float)(yPosition + NodeScale) * terrainScale));

            /////////////////////////////////////////////////////////////////////
            //Set node Depth.
            NodeDepth = nodeDepth;
        }
    }
}
/////////////////////////////////////////////////////////////////////
//Quasar
/////////////////////////////////////////////////////////////////////