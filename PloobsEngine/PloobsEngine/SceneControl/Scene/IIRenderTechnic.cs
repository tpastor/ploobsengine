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
namespace PloobsEngine.SceneControl.Scene
{
    /// <summary>
    /// IRender Technic Specification
    /// </summary>
    public interface IIRenderTechnic
    {
        /// <summary>
        /// Adds the post effect.
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        void AddPostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
        /// <summary>
        /// Determines whether [contains post effect] [the specified post effect].
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        /// <returns>
        ///   <c>true</c> if [contains post effect] [the specified post effect]; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsPostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
        /// <summary>
        /// Removes the post effect.
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        void RemovePostEffect(global::PloobsEngine.SceneControl.IPostEffect postEffect);
        /// <summary>
        /// Cleans up.
        /// </summary>
        void CleanUp();
    }
}
