#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Globalization;
#endregion

namespace ModelImporter
{
    /// <summary>
    /// http://assimp.sourceforge.net/lib_html/index.html
    /// </summary>
    [ContentImporter(new string[] { ".bvh", ".ply", ".nff", ".smd", ".vta", ".obj", ".ms3d", ".dae", ".blend", ".3ds", ".ase", ".dxf", ".md1", ".md2", ".md3", ".pk3", ".md5mesh", ".ter", ".lwo", ".lws", ".cob" }, CacheImportedData = true, DefaultProcessor = "ModelProcessor", DisplayName = "Ploobs Model Importer")]
    public class ModelImporter : ContentImporter<NodeContent>
    {
        
        #region Variables

        private static aiPostProcessSteps ppsteps =
            aiPostProcessSteps.aiProcess_CalcTangentSpace | // calculate tangents and bitangents if possible
            aiPostProcessSteps.aiProcess_JoinIdenticalVertices | // join identical vertices/ optimize indexing
            aiPostProcessSteps.aiProcess_ValidateDataStructure | // perform a full validation of the loader's output
            aiPostProcessSteps.aiProcess_ImproveCacheLocality | // improve the cache locality of the output vertices
            aiPostProcessSteps.aiProcess_RemoveRedundantMaterials | // remove redundant materials
            aiPostProcessSteps.aiProcess_FindDegenerates | // remove degenerated polygons from the import
            aiPostProcessSteps.aiProcess_FindInvalidData | // detect invalid model data, such as invalid normal vectors
            aiPostProcessSteps.aiProcess_GenUVCoords | // convert spherical, cylindrical, box and planar mapping to proper UVs            
            aiPostProcessSteps.aiProcess_FindInstances | // search for instanced meshes and remove them by references to one master
            aiPostProcessSteps.aiProcess_LimitBoneWeights | // limit bone weights to 4 per vertex
            aiPostProcessSteps.aiProcess_OptimizeMeshes | // join small meshes, if possible;            
            (aiPostProcessSteps)0;

        // Provides the logger and allows for tracking of file dependencies
        ContentImporterContext importerContext;


        // The root NodeContent of our model
        private NodeContent rootNode;        

        // The current mesh being constructed
        private MeshBuilder meshBuilder;

        // Indices of vertex channels for the current mesh
        private int textureCoordinateDataIndex;
        private int colorCoordinateDataIndex;         
        private int normalDataIndex;
        private int tangentDataIndex;
        private int boneDataIndex;
        private int binormalDataIndex;
        aiScene scene;
        #endregion

        #region Entry point
        
        String directory = null;
        String filename;
        /// <summary>
        /// The importer's entry point.
        /// Called by the framework when importing a game asset.
        /// </summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">
        /// Contains information for importing a game asset, such as a logger interface.
        /// </param>
        /// <returns>Resulting game asset.</returns>
        public override NodeContent Import(string filename,
            ContentImporterContext context)
        {
            // Uncomment the following line to debug:
            //System.Diagnostics.Debugger.Launch();

            importerContext = context;            

            // Reset all importer state
            // See field declarations for more information
            rootNode = new NodeContent();
            meshBuilder = null;
            this.filename = filename;
            // Model identity is tied to the file it is loaded from
            rootNode.Identity = new ContentIdentity(filename);
            
            var flags = (ppsteps |
                aiPostProcessSteps.aiProcess_GenSmoothNormals | // generate smooth normal vectors if not existing
                aiPostProcessSteps.aiProcess_SplitLargeMeshes | // split large, unrenderable meshes into submeshes
                aiPostProcessSteps.aiProcess_Triangulate | // triangulate polygons with more than 3 edges
                aiPostProcessSteps.aiProcess_ConvertToLeftHanded | // convert everything to D3D left handed space
                aiPostProcessSteps.aiProcess_SortByPType | // make 'clean' meshes which consist of a single typ of primitives
                aiPostProcessSteps.aiProcess_FixInfacingNormals |
                aiPostProcessSteps.aiProcess_FlipWindingOrder |
                (aiPostProcessSteps)0);

            Importer importer = new Importer();

            scene = importer.ReadFile(filename, flags);
            if (scene != null)
            {
                directory = Path.GetDirectoryName(filename);
            }
            else
            {
                throw new InvalidContentException("Failed to open file: " + filename + ". Either Assimp screwed up or the path is not valid.");
            }


            ///animacoes
            Dictionary<String, AnimationContent> AnimNameToAcontent = new Dictionary<string, AnimationContent>();
            for (int i = 0; i < scene.mNumAnimations; i++)
            {
                AnimationContent AnimationContent = new AnimationContent();
                AnimationContent.Duration = TimeSpan.FromMilliseconds(scene.mAnimations[i].mDuration);
                if (String.IsNullOrEmpty(scene.mAnimations[i].mName.Data))
                    scene.mAnimations[i].mName.Data = "EMPTY";
                AnimationContent.Name = scene.mAnimations[i].mName.Data;

                AnimNameToAcontent.Add(scene.mAnimations[i].mName.Data, AnimationContent);

                for (int j = 0; j < scene.mAnimations[i].mNumChannels; j++)
                {
                    AnimationChannel AnimationChannel;
                    String boneName = scene.mAnimations[i].mChannels[j].mNodeName.Data;

                    if (AnimationContent.Channels.ContainsKey(boneName))
                    {
                        AnimationChannel = AnimationContent.Channels[boneName];
                    }
                    else
                    {
                        AnimationChannel = new AnimationChannel();
                        AnimationContent.Channels.Add(boneName, AnimationChannel);
                    }

                    Dictionary<double, Vector3> position = new Dictionary<double, Vector3>();
                    Dictionary<double, Vector3> scales = new Dictionary<double, Vector3>();
                    Dictionary<double, Quaternion> rots = new Dictionary<double, Quaternion>();

                    SortedSet<double> set = new SortedSet<double>();
                    for (int w = 0; w < scene.mAnimations[i].mChannels[j].mNumPositionKeys; w++)
                    {
                        position.Add(scene.mAnimations[i].mChannels[j].mPositionKeys[w].mTime, new Vector3(scene.mAnimations[i].mChannels[j].mPositionKeys[w].mValue.x, scene.mAnimations[i].mChannels[j].mPositionKeys[w].mValue.y, scene.mAnimations[i].mChannels[j].mPositionKeys[w].mValue.z));
                        set.Add(scene.mAnimations[i].mChannels[j].mPositionKeys[w].mTime);
                    }

                    for (int w = 0; w < scene.mAnimations[i].mChannels[j].mNumScalingKeys; w++)
                    {
                        scales.Add(scene.mAnimations[i].mChannels[j].mScalingKeys[w].mTime, new Vector3(scene.mAnimations[i].mChannels[j].mScalingKeys[w].mValue.x, scene.mAnimations[i].mChannels[j].mScalingKeys[w].mValue.y, scene.mAnimations[i].mChannels[j].mScalingKeys[w].mValue.z));
                        set.Add(scene.mAnimations[i].mChannels[j].mScalingKeys[w].mTime);
                    }

                    for (int w = 0; w < scene.mAnimations[i].mChannels[j].mNumRotationKeys; w++)
                    {
                        rots.Add(scene.mAnimations[i].mChannels[j].mRotationKeys[w].mTime, new Quaternion(scene.mAnimations[i].mChannels[j].mRotationKeys[w].mValue.x, scene.mAnimations[i].mChannels[j].mRotationKeys[w].mValue.y, scene.mAnimations[i].mChannels[j].mRotationKeys[w].mValue.z, scene.mAnimations[i].mChannels[j].mRotationKeys[w].mValue.w));
                        set.Add(scene.mAnimations[i].mChannels[j].mRotationKeys[w].mTime);
                    }

                    foreach (var item in set)
                    {
                        Vector3 pos;
                        Vector3 sca;
                        Quaternion rot;

                        if (position.TryGetValue(item, out pos) == false)
                        {
                            pos = Vector3.Zero;
                        }
                        if (rots.TryGetValue(item, out rot) == false)
                        {
                            rot = Quaternion.Identity;
                        }
                        if (scales.TryGetValue(item, out sca) == false)
                        {
                            sca = Vector3.One;
                        }

                        Matrix world = Matrix.CreateScale(sca) * Matrix.CreateFromQuaternion(rot) * Matrix.CreateTranslation(pos);

                        AnimationChannel.Add(new AnimationKeyframe(TimeSpan.FromMilliseconds(item), world));
                    }
                }
            }

            rootNode.Children.Add(extractNo(scene.mRootNode));

            aiNode boneRoot = null;
            aiBone broot = null;
            int distance = int.MaxValue;
            ///pega o root manow
            foreach (var item in bones)
            {
                aiNode ainode = scene.mRootNode.FindNode(item.mName);
                aiNode n = ainode;
                int d = 0;
                //find the closest to the root =P
                while (n.mParent != null)
                {
                     d++;
                     n = n.mParent;
                }
                if (d < distance)
                {
                    broot = item;
                    boneRoot = ainode;
                    distance = d;
                }
            }

            if (broot != null)
            {

                BoneContent BoneContent = new BoneContent();
                BoneContent.Name = broot.mName.Data;
                BoneContent.Transform = tomatrix(broot.mOffsetMatrix);
                rootNode.Children.Add(BoneContent);

                foreach (var item2 in AnimNameToAcontent)
                {
                    BoneContent.Animations.Add(item2.Key, item2.Value);
                }

                foreach (var item in boneRoot.mChildren)
                {
                    BoneContent.Children.Add(createSkel(boneRoot));
                }

                ClearTree(rootNode);
            }

            

            return rootNode;
        }

        private BoneContent createSkel(aiNode root)
        {
            BoneContent BoneContent = null;
            if (ainodetocontent.ContainsKey(root.mName.Data))
            {
                aiBone b = bones.Find( (a) => a.mName.Data == root.mName.Data);
                if (b != null)
                {
                    BoneContent = new BoneContent();
                    BoneContent.Name = b.mName.Data;
                    BoneContent.Transform = tomatrix(b.mOffsetMatrix);

                    foreach (var item in root.mChildren)
                    {
                        BoneContent bon = createSkel(item);
                        if (bon != null)
                        {
                            BoneContent.Children.Add(bon);
                        }
                    }
                    return BoneContent;
                }
            }
            return null;
        }

        List<aiBone> bones = new List<aiBone>();
        Dictionary<String, NodeContent> ainodetocontent = new Dictionary<String, NodeContent>();
        Dictionary<int,List<KeyValuePair<String,float>>> wbone = new Dictionary<int,List<KeyValuePair<string,float>>>();

        private NodeContent extractNo(aiNode node)
        {
                    NodeContent nodeContent = new NodeContent();
                    nodeContent.Name = node.mName.Data;
                    nodeContent.Transform = tomatrix(node.mTransformation);                    
                    ainodetocontent.Add(node.mName.Data, nodeContent);

                    wbone.Clear();
                    for (int i = 0; i < node.mNumMeshes; i++)
                    {
                        for (int j = 0; j < scene.mMeshes[(int)node.mMeshes[i]].mNumBones; j++)
                        {
                            aiBone b = scene.mMeshes[(int)node.mMeshes[i]].mBones[j];
                            for (int w = 0; w < b.mNumWeights; w++)
                            {
                                aiVertexWeight vw = b.GetmWeights()[w];
                                if (!wbone.ContainsKey((int)vw.mVertexId))
                                {
                                    wbone[(int)vw.mVertexId] = new List<KeyValuePair<string, float>>();    
                                }
                                wbone[(int)vw.mVertexId].Add(new KeyValuePair<string, float>(b.mName.Data, vw.mWeight));
                            }
                            bones.Add(b);
                        }

                        log("model " + node.mMeshes[i].ToString());
                        MeshContent MeshContent = ExtractMesh(scene.mMeshes[(int)node.mMeshes[i]]);
                        MeshHelper.SwapWindingOrder(MeshContent);                                                
                        MeshHelper.OptimizeForCache(MeshContent);
                        nodeContent.Children.Add(MeshContent);                        
                    }                    

                    foreach (var item in node.mChildren)
                    {
                        nodeContent.Children.Add(extractNo(item));
                    }
                    return nodeContent;
        }

        private MeshContent ExtractMesh(aiMesh aiMesh)
        {
            if (!String.IsNullOrEmpty(aiMesh.mName.Data))
            {
                log("modelname " + aiMesh.mName.Data);
                meshBuilder = MeshBuilder.StartMesh(aiMesh.mName.Data);
            }
            else
            {                
                meshBuilder = MeshBuilder.StartMesh(Path.GetFileNameWithoutExtension(filename));
            }

            if (!aiMesh.HasPositions())
            {
                throw new Exception("MOdel does not have Position");
            }

            // Add additional vertex channels for texture coordinates and normals
            if (aiMesh.HasTextureCoords(0))
            {
                textureCoordinateDataIndex = meshBuilder.CreateVertexChannel<Vector2>(VertexChannelNames.TextureCoordinate(0));
            }
            else if(aiMesh.HasVertexColors(0))
            {
                colorCoordinateDataIndex = meshBuilder.CreateVertexChannel<Vector4>(VertexChannelNames.Color(0));
            }
            if (aiMesh.HasNormals())
            {
                normalDataIndex = meshBuilder.CreateVertexChannel<Vector3>(VertexChannelNames.Normal());
            }
            if (aiMesh.HasTangentsAndBitangents())
            {
                tangentDataIndex = meshBuilder.CreateVertexChannel<Vector3>(VertexChannelNames.Tangent(0));
                binormalDataIndex = meshBuilder.CreateVertexChannel<Vector3>(VertexChannelNames.Binormal(0));
            }
            if (aiMesh.HasBones())
            {
                boneDataIndex = meshBuilder.CreateVertexChannel<BoneWeightCollection>(VertexChannelNames.Weights(0));
            }

            var numFaces = (int)aiMesh.mNumFaces;
            var numVertices = (int)aiMesh.mNumVertices;
            var aiPositions = aiMesh.mVertices;
            var aiNormals = aiMesh.mNormals;
            var aiTextureCoordsAll = aiMesh.mTextureCoords;
            var aiTextureCoords = (aiTextureCoordsAll != null) ? aiTextureCoordsAll[0] : null;

            for (int j = 0; j < aiMesh.mNumVertices; j++)
            {                
                meshBuilder.CreatePosition(aiMesh.mVertices[j].x, aiMesh.mVertices[j].y, aiMesh.mVertices[j].z);                
            }
            
            meshBuilder.SetMaterial(GetMaterial(aiMesh));
            
            var aiFaces = aiMesh.mFaces;
            var dxIndices = new uint[numFaces * 3];
            for (int k = 0; k < numFaces; ++k)
            {
                var aiFace = aiFaces[k];
                var aiIndices = aiFace.mIndices;
                for (int j = 0; j < 3; ++j)
                {
                    int index = (int)aiIndices[j];
                    if (aiMesh.HasTextureCoords(0))
                    {
                        meshBuilder.SetVertexChannelData(textureCoordinateDataIndex, new Vector2(aiMesh.mTextureCoords[0][index].x, aiMesh.mTextureCoords[0][index].y));
                    }
                    else if (aiMesh.HasVertexColors(0))
                    {
                        meshBuilder.SetVertexChannelData(colorCoordinateDataIndex, new Vector4(aiMesh.mColors[0][index].r, aiMesh.mColors[0][index].g, aiMesh.mColors[0][index].b, aiMesh.mColors[0][index].a));
                    }
                    if (aiMesh.HasNormals())
                    {
                        meshBuilder.SetVertexChannelData(normalDataIndex, new Vector3(aiMesh.mNormals[index].x, aiMesh.mNormals[index].y, aiMesh.mNormals[index].z));
                    }

                    if (aiMesh.HasTangentsAndBitangents())
                    {
                        meshBuilder.SetVertexChannelData(tangentDataIndex, new Vector3(aiMesh.mTangents[index].x, aiMesh.mTangents[index].y, aiMesh.mTangents[index].z));
                        meshBuilder.SetVertexChannelData(binormalDataIndex, new Vector3(aiMesh.mBitangents[index].x, aiMesh.mBitangents[index].y, aiMesh.mBitangents[index].z));
                    }
                    if (aiMesh.HasBones())
                    {                        
                        BoneWeightCollection BoneWeightCollection = new BoneWeightCollection();
                        if(wbone.ContainsKey(index))
                        {
                            foreach (var item in wbone[index])
                            {
                                BoneWeightCollection.Add(new BoneWeight(item.Key,item.Value));    
                            }                            
                        }                        
                        meshBuilder.SetVertexChannelData(boneDataIndex, BoneWeightCollection);
                    }

                    meshBuilder.AddTriangleVertex(index);
                }                
            }        
            
            MeshContent meshContent = meshBuilder.FinishMesh();            
            return meshContent;
        }

        private MaterialContent GetMaterial(aiMesh aiMesh)
        {
            aiString difuse = new aiString();
            scene.mMaterials[(int)aiMesh.mMaterialIndex].GetTextureDiffuse0(difuse);

            if (!String.IsNullOrEmpty(difuse.Data))
            {
                String original  = difuse.Data;
                difuse.Data = Path.GetFileName(difuse.Data);
                importerContext.AddDependency(Path.Combine(directory, difuse.Data));
                
                try
                {
                    BasicMaterialContent materialContent = new BasicMaterialContent();
                    materialContent.Name = difuse.Data;
                    materialContent.Texture = new ExternalReference<TextureContent>(difuse.Data);
                    return materialContent;

                }
                catch (InvalidContentException)
                {
                    // InvalidContentExceptions do not need further processing
                    throw;
                }
                catch (Exception e)
                {
                    // Wrap exception with content identity (includes line number)
                    throw new InvalidContentException(e.ToString());
                }
            }
            return null;

        }

        private void ClearTree(NodeContent  root)
        {
            List<NodeContent> toremove = new List<NodeContent>();            
            foreach (var item in root.Children)
            {
                if (!bones.Exists((a) => item.Name == a.mName.Data))
                {
                    toremove.Add(item);
                }
                else
                {
                    ClearTree(item);
                }
            }
            foreach (var item in toremove)
            {
                root.Children.Remove(item);
            }
        }

        private Matrix tomatrix(aiMatrix4x4 m)
        {
            return new Matrix(m.a1, m.a2, m.a3, m.a4, m.b1, m.b2, m.b3, m.b4, m.c1, m.c2, m.c3, m.c4, m.d1, m.d2, m.d3, m.d4);
        }
        private void log(string mes)
        {
            importerContext.Logger.LogWarning(null, rootNode.Identity,
                            mes);

        }


        #endregion
    }
}
