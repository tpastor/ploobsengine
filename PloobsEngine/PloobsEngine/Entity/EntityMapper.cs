using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using PloobsEngine.MessageSystem;

namespace PloobsEngine.Entity
{
    /// <summary>
    /// Manage all entities
    /// Responsable for setting the ids, managing tags and RecieverEntities    
    /// Its a singleton
    /// </summary>
    public class EntityMapper
    {
            Dictionary<int, Object> ids = new Dictionary<int, object>();
            int a = 1000;
            private EntityMapper() { }
            private static EntityMapper mapper = null;

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <returns></returns>
            public static EntityMapper getInstance()
            {
                if (mapper == null)
                    mapper = new EntityMapper();

                return mapper;

            }


            IDictionary<int, IEntity> IdEntity = new Dictionary<int, IEntity>();
            IDictionary<int, IRecieveMessageEntity> IdRecieveEntity = new Dictionary<int, IRecieveMessageEntity>();

            IDictionary<IRecieveMessageEntity, List<String>> recieveEntityTag = new Dictionary<IRecieveMessageEntity, List<String>>();
            IDictionary<String, IList<IRecieveMessageEntity>> tagRecieveEntity = new Dictionary<String, IList<IRecieveMessageEntity>>();

            private int getNextAvaliableId()
            {
                while (true)
                {
                    if (!ids.ContainsKey(++a))
                    {
                        ids.Add(a, null);
                        return a;
                    }                    
                }
            }

        /// <summary>
        /// Add and entity and sets its id
        /// </summary>
        /// <param name="Agente"></param>
        /// <returns></returns>
        public int AddEntity(IEntity Agente)
            {
                int id;
                if (Agente.GetId() > 0)
                {                    
                    if (ids.ContainsKey(Agente.GetId()))
                    {
                        id = getNextAvaliableId();
                        Agente.SetId(id);
                    }
                    else
                    {
                        id = Agente.GetId();
                        ids.Add(id, null);
                    }
                    IdEntity.Add(id, Agente);
                    if (Agente is IRecieveMessageEntity)
                    {
                        IdRecieveEntity.Add(id, Agente as IRecieveMessageEntity);
                    }
                }
                else
                {
                    id = getNextAvaliableId();
                    Agente.SetId(id);
                    IdEntity.Add(id, Agente);
                    if (Agente is IRecieveMessageEntity)
                    {
                        IdRecieveEntity.Add(id, Agente as IRecieveMessageEntity);
                    }
                }
                return id;
            }

            /// <summary>
            /// Return and entity by id
            /// </summary>
            /// <param name="id">id</param>
            /// <returns></returns>
            public IEntity getEntity(int id)
            {
                if(IdEntity.ContainsKey(id))
                {
                return IdEntity[id];
                }
                throw new Exception("nao foi encontrada a entidade referente a " + id);

            }

            /// <summary>
            /// return a RecieverMessageEntity by ID
            /// </summary>
            /// <param name="id">id</param>
            /// <returns></returns>
            public IRecieveMessageEntity getRecieveEntity(int id)
            {
                if (IdRecieveEntity.ContainsKey(id))
                {
                    return IdRecieveEntity[id];
                }
                throw new Exception("nao foi encontrada a entidade referente a " + id);

            }

            /// <summary>
            /// Add a list of RecieverMessageEntity 
            /// int a tag (id the tag do not exist , create it)
            /// </summary>
            /// <param name="tag">tag</param>
            /// <param name="Agente">agents list</param>
            public void AddgrouptagRecieveEntity(string tag, IList<IRecieveMessageEntity> Agente)
            {

                foreach (IRecieveMessageEntity item in Agente)
                {
                    if (!IdEntity.ContainsKey(item.GetId()))
                    {
                        throw new Exception("instancia ainda nao foi colocada no mapper " + item.ToString() );
                    }
                }

                if (tagRecieveEntity.ContainsKey(tag))
                {
                    foreach (IRecieveMessageEntity item in Agente)
                    {
                        tagRecieveEntity[tag].Add(item);
                    }
                }
                else
                {
                    tagRecieveEntity.Add(tag, Agente);

                }

                foreach (IRecieveMessageEntity var in Agente)
                {
                    if(recieveEntityTag.ContainsKey(var))
                    {
                   recieveEntityTag[var].Add(tag);   
                   }
                    else
                   {
                       recieveEntityTag[var] = new List<string>();
                       recieveEntityTag[var].Add(tag);

                   }

                }
                
         
            }

            /// <summary>
            /// Remove one IRecieveMessageEntity from a tag
            /// </summary>
            /// <param name="tag"></param>
            /// <param name="Agente"></param>
            public void RemovegrouptagRecieveEntity(string tag, IRecieveMessageEntity Agente)
            {
                if (tagRecieveEntity.ContainsKey(tag))
                {
                    tagRecieveEntity[tag].Remove(Agente);
                }
                

            }

            /// <summary>
            /// Add ont IrecieveMessageEntity int a tag(create the tag if sont exist)
            /// </summary>
            /// <param name="tag"></param>
            /// <param name="Agente"></param>
            public void AddgrouptagRecieveEntity(string tag, IRecieveMessageEntity Agente)
            {
                if (!IdEntity.ContainsKey(Agente.GetId()))
                {
                    throw new Exception("instancia ainda nao foi colocada no mapper " + Agente.ToString());
                }

                if (tagRecieveEntity.ContainsKey(tag))
                {
                    tagRecieveEntity[tag].Add(Agente);
                }
                else
                {
                    List<IRecieveMessageEntity> l = new List<IRecieveMessageEntity>();
                    l.Add(Agente);
                    tagRecieveEntity.Add(tag, l);
                }

                if (recieveEntityTag.ContainsKey(Agente))
                {
                    recieveEntityTag[Agente].Add(tag);
                }
                else
                {
                    recieveEntityTag[Agente] = new List<string>();
                    recieveEntityTag[Agente].Add(tag);

                }

            }

            /// <summary>
            /// Return a list of RecieveEntities that has the parameter tag
            /// </summary>
            /// <param name="tag">tag</param>
            /// <returns></returns>
            public IList<IRecieveMessageEntity> GetTagRecieveEntity(string tag)
            {
                if (tagRecieveEntity.ContainsKey(tag))
                {
                    return tagRecieveEntity[tag];
                }
                throw new Exception("Nao contem tag " + tag);

            }

            /// <summary>
            /// return all entities
            /// </summary>
            /// <returns></returns>
            public ICollection<IEntity> GetAllEntities()
            {
                return IdEntity.Values;
            }
            /// <summary>
            /// return all IRecieveMessageEntity entities
            /// </summary>
            /// <returns></returns>
            public ICollection<IRecieveMessageEntity> GetAllRecieveEntities()
            {
                return  IdRecieveEntity.Values;
            }

            /// <summary>
            /// Checks if entity with parameter id exist.
            /// </summary>
            /// <param name="id">The id.</param>
            /// <returns></returns>
            public bool CheckEntity(int id)
            {
                if (!IdEntity.ContainsKey(id))
                {
                     if(!IdRecieveEntity.ContainsKey(id))
                     {
                         return false;
                     }
                }
                return true;
            
            }
            
            
            /// <summary>
            /// Remove the entity
            /// THE ID USED BY THIS ENTITY WILL NOT USED AGAIN (EVEN AFTER THE REMOVE)
            /// </summary>
            /// <param name="cod">id</param>
            public void RemoveEntity(int cod)
            {
                if (IdEntity.ContainsKey(cod))
                {
                    IdEntity.Remove(cod);

                    if (IdRecieveEntity.ContainsKey(cod))
                    {
                        IRecieveMessageEntity irent = IdRecieveEntity[cod];
                        List<String> tags = null;
                        bool found = recieveEntityTag.TryGetValue(irent,out tags);

                        if (found)
                        {
                            foreach (String var in tags)
	                            {
                                    tagRecieveEntity[var].Remove(irent);                            		 
	                            }
                            recieveEntityTag.Remove(irent);
                        }

                                       
                        IdRecieveEntity.Remove(cod);
                    }

                }
                else
                {
                    throw new Exception("Entidade nao encontrada");
                }

            }


            /// <summary>
            /// Clears all entries. (REMOVE ALL ENTITIES)
            /// RESET FUNCTION
            /// </summary>
            public void ClearAllEntries()
            {
                a = 1000;
                ids.Clear();
                IdEntity.Clear();
                IdRecieveEntity.Clear();
                recieveEntityTag.Clear();
                tagRecieveEntity.Clear();

            }
            
            /// <summary>
            /// REMOVE AN ENTITY
            /// </summary>
            /// <param name="ent"></param>
            public void RemoveEntity(IEntity ent)
            {
                int cod = ent.GetId();
                if (IdEntity.ContainsKey(cod))
                {
                    IdEntity.Remove(cod);

                    if (IdRecieveEntity.ContainsKey(cod))
                    {
                        IRecieveMessageEntity irent = IdRecieveEntity[cod];
                        List<String> tags = null;
                        bool found = recieveEntityTag.TryGetValue(irent,out tags);

                        if (found)
                        {
                            foreach (String var in tags)
                            {
                                tagRecieveEntity[var].Remove(irent);
                            }
                            recieveEntityTag.Remove(irent);
                        }

                        IdRecieveEntity.Remove(cod);
                    }                      

                }
                else
                {                    
                    Debug.WriteLine("Entidade nao encontrada Type: " + ent.GetType().AssemblyQualifiedName + " ID: " + ent.GetId());
                }
            }
        }
    }


