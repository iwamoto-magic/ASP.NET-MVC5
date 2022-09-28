using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcBasic.Models;

namespace MvcBasic.Controllers
{
    public class MembersController : Controller
    {
        private MvcBasicContext db = new MvcBasicContext();

        // GET: Members
        //public ActionResult Index()
        //{
        //    return View(db.Members.ToList());
        //}

        // 非同期処理
        // DB処理を待っている間に他のリクエストを処理できるので、アプリケーション全体としてのパフォーマンスが見込める
        public async Task<ActionResult> Index()
        {
            return View(await db.Members.ToListAsync());
        }

        // GET: Members/Details/5
        // リクエストデータ(ポストデータ、クエリー情報) id を 引数で取得
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // HTTPステータスを通知
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // IDをキーにテーブル検索
            Member member = db.Members.Find(id);
            // Find()はデータがない場合はnullを返す
            if (member == null)
            {
                // 404 NotFound を返す
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //入力値をオブジェクトにバインドしている。 Create([Bind(Include = "Id,Name,Email,Birth,Married,Memo")] Member member)
        public ActionResult Create([Bind(Include = "Id,Name,Email,Birth,Married,Memo")] Member member)
        {
            if (ModelState.IsValid)
            {
                // Modelをコンテキストオブジェクトに追加
                db.Members.Add(member);
                // DBに登録
                db.SaveChanges();

                // 処理後に別のアクションを呼び出す事が可能
                // 以下の場合だと、Modekの保存に成功したタイミングで Members/Indexアクション (一覧画面)にリダイレクトさせている。
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Birth,Married,Memo")] Member member)
        {
            if (ModelState.IsValid)
            {
                // 入力値をもとにデータを更新
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")] // アクション名に別名をつけている。これはDeleteだよ 引数が同じだからオーバーロードできない為　新た寝て1つのアクションとして束ねている
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //// 引数idをキーにMembersテーブルを検索
            //Member member = db.Members.Find(id);
            //// 該当するレコードを削除
            //db.Members.Remove(member);

            // ↑Find()使わないパフォーマンスを重視した書き方。DBにアクセスすることなく削除対象行を見つける方法
            var m = new Member() { Id = id };
            db.Entry(m).State = EntityState.Deleted;

            // 削除実行
            db.SaveChanges();
            
            // 処理後はIndexアクションにリダイレクト
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 接続を開放
                db.Dispose();
            }
            // 基底クラスのDisposeメソッドも実行
            base.Dispose(disposing);
        }
    }
}
