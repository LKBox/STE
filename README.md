# STE (Simple Text Editor)
<h4>Basic Usage</h4>
<ul>
  <li>Download the zip file</li>
  <li>Open project file with Visual Studio</li>
  <li>Open <code>STE.API/Web.config</code> file and change the connection string with your database connection information.</li>
  <li>Run the <code>update-database</code> command in Package Manager Console (Select the <code>STE.API</code> as Default Project)</li>
</ul>
<h4>Project Structure</h4>
<p>The solution has two projects. STE.API is a RESTful HTTP Service project and STE.Web is a client project.</p>
<p>STE.Web project uses angular.js for the UI.</p>
<p>Also, API works with an MSSQL database.</p>
<img src="https://raw.githubusercontent.com/LKBox/STE/master/STE.Web/content/structure.png"/>
