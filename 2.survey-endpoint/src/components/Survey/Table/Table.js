import React from "react";
import TableBody from "./TableBody";
import TableHeader from "./TableHeader";

const Table = ({ columns, data, onView }) => {
  return (
    <div className="table-responsive">
      <table className="table table-hover">
        <TableHeader columns={columns} />
        <TableBody data={data} onClick={onView} />
      </table>
    </div>
  );
};

export default React.memo(Table);
