﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellaTodo
{
	public delegate void ModelHandler<IModel>(IModel sender, ModelEventArgs e);

    public class ModelEventArgs : EventArgs
    {
        public CDataCell Item { get; private set; }
        public List<Object> Data { get; set; }

        public ModelEventArgs() { Item = null; }
        public ModelEventArgs(CDataCell dc) { Item = dc; }
    }

    public interface IModelObserver
    {
        void ModelObserver_Event_method(IModel model, ModelEventArgs e);
        void Update_View_Event_method(IModel model, ModelEventArgs e);
        void Update_Add_Task(IModel model, ModelEventArgs e);
        void Update_Delete_Task(IModel model, ModelEventArgs e);
    }

    public interface IModel
	{
        //List<IModelObserver> ObserverList { get; set; }
        void Add_Observer(IModelObserver imo);
        void Remove_Observer(IModelObserver imo);
        void Notify_Observer();
	}
}