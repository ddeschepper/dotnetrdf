﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.io;
using dotSesame = org.openrdf.model;
using dotSesameRepo = org.openrdf.repository;

namespace VDS.RDF.Interop.Sesame
{
    public abstract class BaseRepositoryConnection : dotSesameRepo.RepositoryConnection
    {
        private bool _autoCommit = false;
        private dotSesameRepo.Repository _repo;
        private DotNetRdfValueFactory _factory;
        private SesameMapping _mapping;

        public BaseRepositoryConnection(dotSesameRepo.Repository repository, DotNetRdfValueFactory factory)
        {
            this._repo = repository;
            this._factory = factory;
            this._mapping = new SesameMapping(factory, new dotSesame.impl.GraphImpl());
        }

        protected abstract void AddInternal(Object obj, IEnumerable<Uri> contexts);

        public virtual void add(info.aduna.iteration.Iteration i, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(i.ToTriples(this._mapping));
            this.AddInternal(g, contexts);
        }

        public virtual void add(java.lang.Iterable i, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(new JavaIteratorWrapper<dotSesame.Statement>(i.iterator()).Select(s => SesameConverter.FromSesame(s, this._mapping)));
            this.AddInternal(g, contexts);
        }

        public virtual void add(org.openrdf.model.Statement s, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(SesameConverter.FromSesame(s, this._mapping));
            this.AddInternal(g, contexts);
        }

        public virtual void add(Reader r, string str, org.openrdf.rio.RDFFormat rdff, params org.openrdf.model.Resource[] rarr)
        {
            Object obj = SesameHelper.LoadFromReader(r, str, rdff);
            IEnumerable<Uri> contexts = rarr.ToContexts();
            this.AddInternal(obj, contexts);
        }

        public virtual void add(InputStream @is, string str, org.openrdf.rio.RDFFormat rdff, params org.openrdf.model.Resource[] rarr)
        {
            Object obj = SesameHelper.LoadFromStream(@is, str, rdff);
            IEnumerable<Uri> contexts = rarr.ToContexts();
            this.AddInternal(obj, contexts);
        }

        public virtual void add(org.openrdf.model.Resource r, org.openrdf.model.URI uri, org.openrdf.model.Value v, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(SesameConverter.FromSesameResource(r, this._mapping), SesameConverter.FromSesameUri(uri, this._mapping), SesameConverter.FromSesameValue(v, this._mapping));
            this.AddInternal(g, contexts);
        }

        public virtual void add(File f, string str, org.openrdf.rio.RDFFormat rdff, params org.openrdf.model.Resource[] rarr)
        {
            Object obj = SesameHelper.LoadFromFile(f, str, rdff);
            IEnumerable<Uri> contexts = rarr.ToContexts();
            this.AddInternal(obj, contexts);
        }

        public virtual void add(java.net.URL url, string str, org.openrdf.rio.RDFFormat rdff, params org.openrdf.model.Resource[] rarr)
        {
            Object obj = SesameHelper.LoadFromUri(url, str, rdff);
            IEnumerable<Uri> contexts = rarr.ToContexts();
            this.AddInternal(obj, contexts);
        }

        public abstract void clear(params org.openrdf.model.Resource[] rarr);

        public abstract void clearNamespaces();

        public abstract void close();

        public abstract void commit();

        public void export(org.openrdf.rio.RDFHandler rdfh, params org.openrdf.model.Resource[] rarr)
        {
            throw new NotImplementedException();
        }

        public void exportStatements(org.openrdf.model.Resource r, org.openrdf.model.URI uri, org.openrdf.model.Value v, bool b, org.openrdf.rio.RDFHandler rdfh, params org.openrdf.model.Resource[] rarr)
        {
            throw new NotImplementedException();
        }

        public abstract org.openrdf.repository.RepositoryResult getContextIDs();

        public abstract string getNamespace(string str);

        public abstract org.openrdf.repository.RepositoryResult getNamespaces();

        public org.openrdf.repository.Repository getRepository()
        {
            return this._repo;
        }

        protected abstract dotSesameRepo.RepositoryResult GetStatementsInternal(String sparqlQuery);

        public org.openrdf.repository.RepositoryResult getStatements(org.openrdf.model.Resource r, org.openrdf.model.URI uri, org.openrdf.model.Value v, bool b, params org.openrdf.model.Resource[] rarr)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.model.ValueFactory getValueFactory()
        {
            return this._factory;
        }

        protected abstract bool HasTripleInternal(Triple t);

        public bool hasStatement(org.openrdf.model.Statement s, bool b, params org.openrdf.model.Resource[] rarr)
        {
            Triple t = SesameConverter.FromSesame(s, this._mapping);
            return HasTripleInternal(t);
        }

        public bool hasStatement(org.openrdf.model.Resource r, org.openrdf.model.URI uri, org.openrdf.model.Value v, bool b, params org.openrdf.model.Resource[] rarr)
        {
            throw new NotImplementedException();
        }

        public bool isAutoCommit()
        {
            return this._autoCommit;
        }

        public abstract bool isEmpty();

        public abstract bool isOpen();

        public org.openrdf.query.BooleanQuery prepareBooleanQuery(org.openrdf.query.QueryLanguage ql, string str1, string str2)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.query.BooleanQuery prepareBooleanQuery(org.openrdf.query.QueryLanguage ql, string str)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.query.GraphQuery prepareGraphQuery(org.openrdf.query.QueryLanguage ql, string str1, string str2)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.query.GraphQuery prepareGraphQuery(org.openrdf.query.QueryLanguage ql, string str)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.query.Query prepareQuery(org.openrdf.query.QueryLanguage ql, string str1, string str2)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.query.Query prepareQuery(org.openrdf.query.QueryLanguage ql, string str)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.query.TupleQuery prepareTupleQuery(org.openrdf.query.QueryLanguage ql, string str1, string str2)
        {
            throw new NotImplementedException();
        }

        public org.openrdf.query.TupleQuery prepareTupleQuery(org.openrdf.query.QueryLanguage ql, string str)
        {
            throw new NotImplementedException();
        }

        protected abstract void RemoveInternal(Object obj, IEnumerable<Uri> contexts);

        public virtual void remove(info.aduna.iteration.Iteration i, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(i.ToTriples(this._mapping));
            this.RemoveInternal(g, contexts);
        }

        public virtual void remove(java.lang.Iterable i, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(new JavaIteratorWrapper<dotSesame.Statement>(i.iterator()).Select(s => SesameConverter.FromSesame(s, this._mapping)));
            this.RemoveInternal(g, contexts);
        }

        public virtual void remove(org.openrdf.model.Statement s, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(SesameConverter.FromSesame(s, this._mapping));
            this.RemoveInternal(g, contexts);
        }

        public virtual void remove(org.openrdf.model.Resource r, org.openrdf.model.URI uri, org.openrdf.model.Value v, params org.openrdf.model.Resource[] rarr)
        {
            IEnumerable<Uri> contexts = rarr.ToContexts();
            Graph g = new Graph();
            g.Assert(SesameConverter.FromSesameResource(r, this._mapping), SesameConverter.FromSesameUri(uri, this._mapping), SesameConverter.FromSesameValue(v, this._mapping));
            this.RemoveInternal(g, contexts);
        }

        public abstract void removeNamespace(string str);

        public abstract void rollback();

        public void setAutoCommit(bool b)
        {
            this._autoCommit = b;
        }

        public abstract void setNamespace(string str1, string str2);

        public abstract long size(params org.openrdf.model.Resource[] rarr);
    }
}