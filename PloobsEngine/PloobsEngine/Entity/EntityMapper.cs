#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.MessageSystem;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Utils;
using PloobsEngine.Utils.WeakReference;

namespace PloobsEngine.Entity
{
    /// <summary>
    /// Manage all entities
    /// Responsable for setting the ids, managing tags and RecieverEntities    
    /// Its a singleton
    /// IT USES WEAK REFERENCES !!! (Even so, YOU ARE RESPONSIBLE FOR REMOVING ENTITIES NOT USED !!!!!!!!!)
    /// Weak reference is just a fail safe mechanism =P
    /// </summary>
    public class EntityMapper
    {
            Dictionary<long, object> ids = new Dictionary<long, object>();
            long a = 1000;
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


            IDictionary<long, PloobsEngine.Utils.WeakReference<IEntity>> IdEntity = new Dictionary<long, PloobsEngine.Utils.WeakReference<IEntity>>();
            IDictionary<long, PloobsEngine.Utils.WeakReference<IRecieveMessageEntity>> IdRecieveEntity = new Dictionary<long, PloobsEngine.Utils.WeakReference<IRecieveMessageEntity>>();

            IDictionary<long, IList<String>> recieveEntityTag = new Dictionary<long, IList<String>>();
            IDictionary<String, WeakList<IRecieveMessageEntity>> tagRecieveEntity = new Dictionary<String, WeakList<IRecieveMessageEntity>>();

            private long getNextAvaliableId()
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
            public long AddEntity(IEntity Agente)
            {
                long id;
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
                    IdEntity.Add(id, new PloobsEngine.Utils.WeakReference<IEntity>(Agente));
                    if (Agente is IRecieveMessageEntity)
                    {
                        IdRecieveEntity.Add(id, new PloobsEngine.Utils.WeakReference<IRecieveMessageEntity>(Agente as IRecieveMessageEntity));
                    }
                }
                else
                {
                    id = getNextAvaliableId();
                    Agente.SetId(id);
                    IdEntity.Add(id, new PloobsEngine.Utils.WeakReference<IEntity>(Agente));
                    if (Agente is IRecieveMessageEntity)
                    {
                        IdRecieveEntity.Add(id, new PloobsEngine.Utils.WeakReference<IRecieveMessageEntity>(Agente as IRecieveMessageEntity));
                    }
                }
                return id;
            }

            /// <summary>
            /// Return and entity by id
            /// </summary>
            /// <param name="id">id</param>
            /// <returns></returns>
            public IEntity getEntity(long id)
            {
                if(IdEntity.ContainsKey(id))
                {
                    IEntity IEntity = IdEntity[id].Target ;
                    if (IEntity != null)
                    {
                        return IEntity;
                    }
                }
                throw new Exception("Reference not found for ID" + id);
            }

            /// <summary>
            /// return a RecieverMessageEntity by ID
            /// </summary>
            /// <param name="id">id</param>
            /// <returns></returns>
            public IRecieveMessageEntity getRecieveEntity(long id)
            {
                if (IdRecieveEntity.ContainsKey(id))
                {
                    IRecieveMessageEntity IRecieveMessageEntity = IdRecieveEntity[id].Target;
                    if(IRecieveMessageEntity != null)
                        return IRecieveMessageEntity;
                }
                throw new Exception("Reference not found for ID" + id);

            }

            /// <summary>
            /// Add a list of RecieverMessageEntity 
            /// int a tag (id the tag do not exist , create it)
            /// </summary>
            /// <param name="tag">tag</param>
            /// <param name="Agente">agents list</param>
            public void AddgrouptagRecieveEntity(string tag, WeakList<IRecieveMessageEntity> Agente)
            {

                foreach (IRecieveMessageEntity item in Agente)
                {
                    if (!IdEntity.ContainsKey(item.GetId()))
                    {
                        throw new Exception("Add the following instace to the mapper before adding to a group: " + item.ToString() );
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
                    if(recieveEntityTag.ContainsKey(var.GetId()))
                    {
                        recieveEntityTag[var.GetId()].Add(tag);   
                   }
                    else
                   {
                       recieveEntityTag[var.GetId()] = new List<string>();
                       recieveEntityTag[var.GetId()].Add(tag);
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
            /// Add the IrecieveMessageEntity in a tag (create the tag if doesnt exists)
            /// </summary>
            /// <param name="tag"></param>
            /// <param name="Agente"></param>
            public void AddgrouptagRecieveEntity(string tag, IRecieveMessageEntity Agente)
            {
                if (!IdEntity.ContainsKey(Agente.GetId()))
                {
                    throw new Exception("The following instance is not in the mapper, add it beforing calling this: " + Agente.ToString());
                }

                if (tagRecieveEntity.ContainsKey(tag))
                {
                    tagRecieveEntity[tag].Add(Agente);
                }
                else
                {
                    WeakList<IRecieveMessageEntity> l = new WeakList<IRecieveMessageEntity>();
                    l.Add(Agente);
                    tagRecieveEntity.Add(tag, l);
                }

                if (recieveEntityTag.ContainsKey(Agente.GetId()))
                {
                    recieveEntityTag[Agente.GetId()].Add(tag);
                }
                else
                {
                    recieveEntityTag[Agente.GetId()] = new List<string>();
                    recieveEntityTag[Agente.GetId()].Add(tag);
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
                ActiveLogger.LogMessage("Tag does not exists: " + tag, LogLevel.FatalError);                
                throw new Exception("Tag does not exists: " + tag);
            }

            /// <summary>
            /// return all entities
            /// </summary>
            /// <returns></returns>
            public ICollection<IEntity> GetAllEntities()
            {
                List<IEntity> entities = new List<IEntity>();
                foreach (var item in IdEntity.Values)
                {
                    IEntity e = item.Target;
                    if (e != null)
                        entities.Add(e);
                }
                return entities;                
            }
            /// <summary>
            /// return all IRecieveMessageEntity entities
            /// </summary>
            /// <returns></returns>
            public ICollection<IRecieveMessageEntity> GetAllRecieveEntities()
            {
                List<IRecieveMessageEntity> IRecieveMessageEntity = new List<IRecieveMessageEntity>();
                foreach (var item in IdRecieveEntity.Values)
                {
                    IRecieveMessageEntity i = item.Target;
                    if (i != null)
                        IRecieveMessageEntity.Add(i);
                }
                return IRecieveMessageEntity;
            }

            /// <summary>
            /// Checks if entity with parameter id exists.
            /// </summary>
            /// <param name="id">The id.</param>
            /// <returns></returns>
            public bool CheckEntity(long id)
            {
                if (!IdEntity.ContainsKey(id))
                {
                    return false;                     
                }

                if (IdEntity[id].IsAlive)
                {
                    return true;
                }
                else
                {
                    IdEntity.Remove(id);
                    return false;
                }
            }


            /// <summary>
            /// Remove the entity
            /// THE ID USED BY THIS ENTITY WILL NOT USED AGAIN (EVEN AFTER THE REMOVE)
            /// </summary>
            /// <param name="cod">id</param>
            /// <returns></returns>
            public bool RemoveEntity(long cod)
            {
                if (IdEntity.ContainsKey(cod))
                {
                    IdEntity.Remove(cod);

                    if (IdRecieveEntity.ContainsKey(cod))
                    {
                        IRecieveMessageEntity irent = IdRecieveEntity[cod].Target;
                        if (irent == null)
                        {
                            IdRecieveEntity.Remove(cod);
                            return false;
                        }

                        IList<String> tags = null;
                        bool found = recieveEntityTag.TryGetValue(irent.GetId(), out tags);

                        if (found)
                        {
                            foreach (String var in tags)
	                            {
                                    tagRecieveEntity[var].Remove(irent);                            		 
	                            }
                            recieveEntityTag.Remove(irent.GetId());
                        }                                       
                        IdRecieveEntity.Remove(cod);
                    }

                }
                else
                {
                    return false;
                }
                return true;
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
            public bool RemoveEntity(IEntity ent)
            {
                long cod = ent.GetId();
                if (IdEntity.ContainsKey(cod))
                {
                    IdEntity.Remove(cod);

                    if (IdRecieveEntity.ContainsKey(cod))
                    {
                        //iam sure it is not collected cause one reference is passed as a parameter ....
                        IRecieveMessageEntity irent = IdRecieveEntity[cod].Target;
                        
                        IList<String> tags = null;
                        bool found = recieveEntityTag.TryGetValue(irent.GetId(), out tags);

                        if (found)
                        {
                            foreach (String var in tags)
                            {
                                tagRecieveEntity[var].Remove(irent);
                            }
                            recieveEntityTag.Remove(irent.GetId());
                        }

                        IdRecieveEntity.Remove(cod);
                    }                      

                }
                else
                {
                    return false;                    
                }
                return true;
            }
        }
    }


